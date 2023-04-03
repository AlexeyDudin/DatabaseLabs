using DomainLab3;
using DomainLab3.Models.Dtos;
namespace Application
{
    public interface ICourceService
    {
        public Cource SaveCource(Cource cource);
        public Cource DeleteCource(Guid courceId);
        public Cource GetCourceStatus(GetCourceStatusParamsDto matherialParams);
        public CourceEnrollment SaveEnrollment(CourceEnrollment enrollmentParams);
        public SaveMatherialStatusParamsDto SaveMatherial(SaveMatherialStatusParamsDto matherialParams);
    }
}
