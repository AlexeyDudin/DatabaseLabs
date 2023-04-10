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
            Cource findCource = _courceRepository.Where(c => c.Id == courceId).First();
            _courceRepository.Delete(findCource);
            _unitOfWork.Commit();
            return findCource;
        }

        public Cource GetCourceStatus(GetCourceStatusParamsDto matherialParams)
        {
            Cource findCource = _courceRepository.Where(c => c.Id == matherialParams.CourceId && c.CourceEnrollments.Where(ce => ce.EnrollmentId == matherialParams.EnrollmentId).Any()).FirstOrDefault();
            return findCource;
        }

        public Cource SaveCource(Cource cource)
        {
            foreach (var matherial in cource.CourceMatherials)
            {
                var cm = matherial.CourceModule;

                var module = _moduleRepository.Where(c => c.ModuleId == cm.ModuleId).FirstOrDefault();
                if (module == null)
                    _moduleRepository.Add(cm);
            }
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