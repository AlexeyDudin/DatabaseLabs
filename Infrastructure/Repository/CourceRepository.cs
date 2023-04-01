using DomainLab3.Models.Dtos;
using Infrastructure.DbWorkers;

namespace Infrastructure.Repository
{
    public class CourceRepository : ICourceRepository
    {
        private const string INSERT_COURCE = @"INSERT INTO course VALUES( @courseId );";
        private const string INSERT_MODULE = @"INSERT INTO course_module VALUES(moduleId, courseId, isRequired);";
        private const string GET_ALL_COURCE = @"SELECT * FROM course";
        private const string SOFT_DELETE_COURCE_STATUS = @"
                UPDATE course_status
                SET
                    deleted_at = now()
                FROM course_status AS cs 
                    INNER JOIN course_enrollment AS ce ON cs.enrollment_id = ce.enrollment_id 	
                WHERE course_status.enrollment_id = cs.enrollment_id AND ce.course_id = @courseId;
                ";
        private const string SOFT_DELETE_COURCE_MODULE_STATUS = @"
                UPDATE course_module_status
                SET
                    deleted_at = now()
                FROM course_module_status AS cms 
                    INNER JOIN course_enrollment AS ce ON cms.enrollment_id = ce.enrollment_id 	
                WHERE course_module_status.enrollment_id = ce.enrollment_id AND ce.course_id = @courseId;
                ";
        private const string SOFT_DELETE_COURCE_MODULE = @"
                UPDATE course_module
                SET
                    deleted_at = now()
                WHERE course_id = @courseId;    
                ";
        private const string HARD_DELETE_COURCE_ENROLLMENT = @"
                DELETE FROM course_enrollment WHERE course_id = @courseId;
                ";
        private const string SOFT_DELETE_COURCE = @"
                UPDATE course
                SET
                    deleted_at = now()
                WHERE course_id = @courseId; 
                ";

        private readonly IDbConnection connection;

        public CourceRepository(IDbConnection connection)
        {
            this.connection = connection;
        }

        public SaveCourceParamsDto SaveCourse(SaveCourceParamsDto saveCourseParams)
        {
            var requireModuleIds = saveCourseParams.RequiredModuleIds;
            var moduleIds = saveCourseParams.ModuleIds;

            //Убираем повторы
            foreach (var requireModule in requireModuleIds)
            {
                moduleIds.Remove(requireModule);
            }

            try
            {
                connection.OpenConnection();
                connection.BeginTransaction();

                {
                    List<Parameter> insertCourseParameters = new List<Parameter>
                    {
                        new Parameter("courseId", saveCourseParams.CourceId.ToString())
                    };
                    connection.Execute(INSERT_COURCE, insertCourseParameters);
                }

                for (var i = 0; i < moduleIds.Count; i++)
                {
                    var insertModuleSql = INSERT_MODULE;

                    insertModuleSql = insertModuleSql.Replace("moduleId", "@moduleId" + i);
                    insertModuleSql = insertModuleSql.Replace("courseId", "@courseId" + i);
                    insertModuleSql = insertModuleSql.Replace("isRequired", "@isRequired" + i);

                    List<Parameter> insertModuleParameters = new List<Parameter>();
                    insertModuleParameters.Add(new Parameter("moduleId" + i, moduleIds[i].ToString()));
                    insertModuleParameters.Add(new Parameter("courseId" + i, saveCourseParams.CourceId.ToString()));
                    insertModuleParameters.Add(new Parameter("isRequired" + i, "false"));
                    connection.Execute(insertModuleSql, insertModuleParameters);
                }

                // some execute commands
                connection.Commit();
            }
            catch (Exception ex)
            {
                connection.Rollback();
                throw new Exception(ex.Message);
            }
            finally
            {
                connection.CloseConnection();
            }

            return saveCourseParams;
        }
    }
}
