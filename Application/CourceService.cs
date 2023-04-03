using Application.Exceptions;
using DomainLab3;
using DomainLab3.Models.Dtos;
using Infrastructure.Repository;

namespace Application
{
    public class CourceService : ICourceService
    {
        private readonly ICourceRepository _courceRepository;
        public CourceService(ICourceRepository courceRepository)
        {
            _courceRepository = courceRepository;
        }

        public Cource DeleteCource(Guid courceId)
        {
            Cource result = _courceRepository.GetCourceById(courceId);
            if (result == null)
                throw new CourceNotFoundException($"Курс с идентификатором {courceId} не найден");
            _courceRepository.DeleteCource(courceId);
            return result;
        }

        public Cource GetCourceStatus(GetCourceStatusParamsDto matherialParams)
        {
            Cource cource = _courceRepository.GetFullCourceInfoByStatus(matherialParams.CourceId);
            if (cource == null)
                throw new CourceNotFoundException($"Курс с идентификатором {matherialParams.CourceId} не найден");
            if (cource.CourceEnrollments == null || !cource.CourceEnrollments.Where(e => e.EnrollmentId == matherialParams.EnrollmentId).Any())
                throw new CourceNotFoundException($"Курс с параметрами courceId = {matherialParams.CourceId} и enrollmentId = {matherialParams.EnrollmentId} не найден");
            return cource;
        }

        public Cource SaveCource(Cource cource)
        {
            return _courceRepository.SaveCourse(cource);
        }

        public CourceEnrollment SaveEnrollment(CourceEnrollment enrollmentParams)
        {
            Cource cource = _courceRepository.GetFullCourceInfoByStatus(enrollmentParams.CourceId);
            if (cource == null)
                throw new CourceNotFoundException($"Курс с идентификатором {enrollmentParams.CourceId} не найден");
            return _courceRepository.SaveEnrollment(enrollmentParams, cource);
        }

        public CourceModule SaveMatherial(CourceModule matherialParams)
        {
            var moduleStatus = _courceRepository.GetModuleStatus(matherialParams);

            moduleStatus.Duration += matherialParams.Duration;
            if (moduleStatus.Matherial.IsRequired)
                moduleStatus.Progress = matherialParams.Progress;
            else
                moduleStatus.Progress = 100;

            _courceRepository.UpdateMatherialStatus(moduleStatus);
            return moduleStatus;
        }
    }
}
