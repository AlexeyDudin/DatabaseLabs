using DomainLab3;
using DomainLab3.Models.Dtos;
using Infrastructure.Converters;
using Infrastructure.DbWorkers;

namespace Infrastructure.Repository
{
    public class CourceRepository : ICourceRepository
    {
        private const string INSERT_COURCE = @"INSERT INTO course([course_id]) VALUES( @courseId );";
        private const string INSERT_MODULE = @"INSERT INTO course_matherial([module_id], [course_id], [is_required]) VALUES(@moduleId, @courseId, @isRequired);";
        private const string GET_ALL_ACTIVE_COURCE = @"SELECT * FROM course WHERE [deleted_at] IS NULL";
        private const string GET_COURCE_BY_ID = @"SELECT * FROM course WHERE [course_id] = @courseId AND [deleted_at] IS NULL";
        private const string SOFT_DELETE_COURCE_STATUS = @"
                UPDATE course_status
                SET
                    deleted_at = GETUTCDATE()
                FROM course_status
                WHERE [enrollment_id] IN
                (
                    SELECT enrollment_id
                    FROM [course_enrollment]
                    WHERE [course_id] = @courseId
                );
                ";
        private const string SOFT_DELETE_COURCE_MODULE_STATUS = @"
                UPDATE course_module_status
                SET
                    deleted_at = GETUTCDATE()
                FROM course_module_status 	
                WHERE [enrollment_id] IN 
                (
                    SELECT [enrollment_id]
                    FROM [course_enrollment]
                    WHERE [course_id] = @courseId
                );
                ";
        private const string SOFT_DELETE_COURCE_MATHERIAL = @"
                UPDATE course_matherial
                SET
                    deleted_at = GETUTCDATE()
                WHERE course_id = @courseId;    
                ";
        private const string HARD_DELETE_COURCE_ENROLLMENT = @"
                DELETE FROM course_enrollment WHERE course_id = @courseId;
                ";
        private const string SOFT_DELETE_COURCE = @"
                UPDATE course
                SET
                    deleted_at = GETUTCDATE()
                WHERE course_id = @courseId; 
                ";

        private readonly IDbConnection connection;

        public CourceRepository(IDbConnection connection)
        {
            this.connection = connection;
        }

        public void DeleteCource(Guid courceId)
        {
            try
            {
                connection.OpenConnection();
                connection.BeginTransaction();
                {
                    List<Parameter> insertCourseParameters = new List<Parameter>
                    {
                        new Parameter("courseId", courceId.ToString())
                    };
                    connection.Execute(SOFT_DELETE_COURCE_STATUS, insertCourseParameters);
                }
                {
                    List<Parameter> insertCourseParameters = new List<Parameter>
                    {
                        new Parameter("courseId", courceId.ToString())
                    };
                    connection.Execute(SOFT_DELETE_COURCE_MODULE_STATUS, insertCourseParameters);
                }
                {
                    List<Parameter> insertCourseParameters = new List<Parameter>
                    {
                        new Parameter("courseId", courceId.ToString())
                    };
                    connection.Execute(SOFT_DELETE_COURCE_MATHERIAL, insertCourseParameters);
                }
                {
                    List<Parameter> insertCourseParameters = new List<Parameter>
                    {
                        new Parameter("courseId", courceId.ToString())
                    };
                    connection.Execute(HARD_DELETE_COURCE_ENROLLMENT, insertCourseParameters);
                }
                {
                    List<Parameter> insertCourseParameters = new List<Parameter>
                    {
                        new Parameter("courseId", courceId.ToString())
                    };
                    connection.Execute(SOFT_DELETE_COURCE, insertCourseParameters);
                }
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
        }

        public Cource GetCourceById(Guid courceId)
        {
            Cource result = null;
            try
            {
                connection.OpenConnection();
                connection.BeginTransaction();
                List<Parameter> insertCourseParameters = new List<Parameter>
                {
                    new Parameter("courseId", courceId.ToString())
                };
                result = connection.Execute(GET_COURCE_BY_ID, insertCourseParameters).ConvertToCource();
            }
            catch (Exception ex)
            {
                connection.Rollback();
                result = null;
                throw new Exception(ex.Message);
            }
            finally
            {
                connection.CloseConnection();
            }
            return result;
        }

        public Cource SaveCourse(Cource cource)
        {
            try
            {
                connection.OpenConnection();
                connection.BeginTransaction();

                {
                    List<Parameter> insertCourseParameters = new List<Parameter>
                    {
                        new Parameter("courseId", cource.Id.ToString())
                    };
                    connection.Execute(INSERT_COURCE, insertCourseParameters);
                }

                for (var i = 0; i < cource.CourceMatherials.Count; i++)
                {
                    var insertModuleSql = INSERT_MODULE;

                    insertModuleSql = insertModuleSql.Replace("@moduleId", "@moduleId" + i);
                    insertModuleSql = insertModuleSql.Replace("@courseId", "@courseId" + i);
                    insertModuleSql = insertModuleSql.Replace("@isRequired", "@isRequired" + i);

                    List<Parameter> insertModuleParameters = new List<Parameter>();
                    insertModuleParameters.Add(new Parameter("moduleId" + i, cource.CourceMatherials[i].ModuleId.ToString()));
                    insertModuleParameters.Add(new Parameter("courseId" + i, cource.Id.ToString()));
                    insertModuleParameters.Add(new Parameter("isRequired" + i, cource.CourceMatherials[i].IsRequired));
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

            return cource;
        }
    }
}
