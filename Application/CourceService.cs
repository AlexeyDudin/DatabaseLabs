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
                throw new CourceNotFoudException($"Курс с идентификатором {courceId} не найден");
            _courceRepository.DeleteCource(courceId);
            return result;
        }

        public CourceStatusDataDto GetCourceStatus(GetCourceStatusParamsDto matherialParams)
        {
            throw new NotImplementedException();
        }

        public Cource SaveCource(Cource cource)
        {
            return _courceRepository.SaveCourse(cource);
        }

        public SaveEnrollmentParamsDto SaveEnrollment(SaveEnrollmentParamsDto enrollmentParams)
        {
            throw new NotImplementedException();
        }

        public SaveMatherialStatusParamsDto SaveMatherial(SaveMatherialStatusParamsDto matherialParams)
        {
            throw new NotImplementedException();
        }
    }
}
