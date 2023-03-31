using Application.Models.Dtos;
namespace Application
{
    public interface ICourceService
    {
        public SaveCourceParamsDto SaveCource(SaveCourceParamsDto courceParams);
        public SaveCourceParamsDto DeleteCource(Guid courceId);
        public CourceStatusDataDto GetCourceStatus(GetCourceStatusParamsDto matherialParams);
        public SaveEnrollmentParamsDto SaveEnrollment(SaveEnrollmentParamsDto enrollmentParams);
        public SaveMatherialStatusParamsDto SaveMatherial(SaveMatherialStatusParamsDto matherialParams);
    }
}
