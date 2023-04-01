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

        public SaveCourceParamsDto DeleteCource(Guid courceId)
        {
            throw new NotImplementedException();
        }

        public CourceStatusDataDto GetCourceStatus(GetCourceStatusParamsDto matherialParams)
        {
            throw new NotImplementedException();
        }

        public SaveCourceParamsDto SaveCource(SaveCourceParamsDto courceParams)
        {
            return _courceRepository.SaveCourse(courceParams);
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
