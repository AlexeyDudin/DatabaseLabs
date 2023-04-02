using DomainLab3;
using DomainLab3.Models.Dtos;
using Lab3.ResponceWorker;

namespace Lab3
{
    public interface ICourceApiService
    {
        Responce SaveCource(Cource cource);
        Responce DeleteCource(Guid courceId);
        Responce GetCourceStatus(GetCourceStatusParamsDto matherialParams);
        Responce SaveEnrollment(SaveEnrollmentParamsDto enrollmentParams);
        Responce SaveMatherial(SaveMatherialStatusParamsDto matherialParams);
    }
}
