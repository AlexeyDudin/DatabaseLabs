using Application;
using DomainLab3;
using DomainLab3.Models.Dtos;
using Infrastructure.Repository;
using InfrastructureLab4.Repositories.Interfaces;

namespace ApplicationLab4
{
    public class CourceServiceLab4 : ICourceService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<Cource> _courceRepository;
        private readonly IRepository<CourceStatus> _statusRepository;
        private readonly IRepository<CourceModule> _moduleRepository;
        
        public CourceServiceLab4(IUnitOfWork unitOfWork) 
        {
            _unitOfWork = unitOfWork;
            _courceRepository = _unitOfWork.CourceRepository;
            _statusRepository = _unitOfWork.CourceStatusRepository;
            _moduleRepository = _unitOfWork.CourceModuleRepository;
        }

        public Cource DeleteCource(Guid courceId)
        {
            throw new NotImplementedException();
        }

        public Cource GetCourceStatus(GetCourceStatusParamsDto matherialParams)
        {
            throw new NotImplementedException();
        }

        public Cource SaveCource(Cource cource)
        {
            _courceRepository.Add(cource);
            _unitOfWork.Commit();
            return cource;
        }

        public CourceEnrollment SaveEnrollment(CourceEnrollment enrollmentParams)
        {
            throw new NotImplementedException();
        }

        public CourceModule SaveMatherial(CourceModule matherialParams)
        {
            throw new NotImplementedException();
        }
    }
}