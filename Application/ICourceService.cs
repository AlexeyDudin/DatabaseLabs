using DomainLab3;
using DomainLab3.Models.Dtos;
namespace Application
{
    public interface ICourceService
    {
        public Cource SaveCource(Cource cource);
        public Cource DeleteCource(Guid courceId);
        public CourceStatusDataDto GetCourceStatus(GetCourceStatusParamsDto matherialParams);
        public SaveEnrollmentParamsDto SaveEnrollment(SaveEnrollmentParamsDto enrollmentParams);
        public SaveMatherialStatusParamsDto SaveMatherial(SaveMatherialStatusParamsDto matherialParams);
    }
}
