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
            Cource cource = _courceRepository.GetFullCourceInfoByStatus(matherialParams);
            if (cource == null)
                throw new CourceNotFoundException($"Курс с идентификатором {matherialParams.CourceId} не найден");
            if (cource.CourceEnrollments == null || !cource.CourceEnrollments.Where(e => e.EnrollmentId == matherialParams.EnrollmentId).Any())
                throw new CourceNotFoundException($"Курс с параметрами courceId = {matherialParams.CourceId} и enrollmentId = {matherialParams.EnrollmentId} не найден");
            return _courceRepository.GetFullCourceInfoByStatus(matherialParams);
        }

        public Cource SaveCource(Cource cource)
        {
            return _courceRepository.SaveCourse(cource);
        }

        public CourceEnrollment SaveEnrollment(CourceEnrollment enrollmentParams)
        {
            return _courceRepository.SaveEnrollment(enrollmentParams);
        }

        public SaveMatherialStatusParamsDto SaveMatherial(SaveMatherialStatusParamsDto matherialParams)
        {
            throw new NotImplementedException();
        }
    }
}
