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
            if (findCource == null)
                throw new NullReferenceException($"Курс с по ключу {courceId} не найден");
            foreach (var enrollment in findCource.CourceEnrollments)
            {
                _statusRepository.Delete(enrollment.CourceStatus);
            }
            findCource.CourceEnrollments.Clear();
            foreach (var module in findCource.CourceMatherials)
            {
                _moduleRepository.Delete(module.CourceModule);
            }
            findCource.CourceMatherials.Clear();
            _courceRepository.Delete(findCource);
            _unitOfWork.Commit();
            return findCource;
        }

        public Cource GetCourceStatus(GetCourceStatusParamsDto matherialParams)
        {
            Cource findCource = _courceRepository.Where(c => c.Id == matherialParams.CourceId && c.CourceEnrollments.Where(ce => ce.EnrollmentId == matherialParams.EnrollmentId).Any()).FirstOrDefault();
            foreach (var enrollment in findCource.CourceEnrollments)
            {
                enrollment.CourceStatus = _statusRepository.Where(s => s.EnrollmentId == enrollment.EnrollmentId).FirstOrDefault();
            }
            return findCource;
        }

        public Cource SaveCource(Cource cource)
        {
            _courceRepository.Add(cource);
            _unitOfWork.Commit();
            return cource;
        }

        public CourceEnrollment SaveEnrollment(CourceEnrollment enrollmentParams)
        {
            var cource = _courceRepository.Where(c => c.Id == enrollmentParams.CourceId).FirstOrDefault();
            if (cource == null)
                throw new NullReferenceException($"Курс с по ключу {enrollmentParams.CourceId} не найден");

            var status = _statusRepository.Where(s => s.EnrollmentId == enrollmentParams.EnrollmentId).FirstOrDefault();
            if (status == null)
            {
                _statusRepository.Add(enrollmentParams.CourceStatus);
                status = enrollmentParams.CourceStatus;
            }

            enrollmentParams.Cource = cource;
            enrollmentParams.CourceStatus = status;

            foreach (var matherial in cource.CourceMatherials)
            {
                CourceModule newModuleToEnrollment = new CourceModule();
                
                newModuleToEnrollment.Matherial = matherial;
                //matherial.CourceModule = newModuleToEnrollment;
                
                newModuleToEnrollment.Enrollment = enrollmentParams;
                enrollmentParams.CourceModule = newModuleToEnrollment;

                newModuleToEnrollment.EnrollmentId = enrollmentParams.EnrollmentId;
                newModuleToEnrollment.ModuleId = matherial.ModuleId;

                _moduleRepository.Add(newModuleToEnrollment);
            }

            //var status = _statusRepository.Where(s => s.EnrollmentId == enrollmentParams.EnrollmentId).FirstOrDefault();
            //if (status == null)
            //    _statusRepository.Add(enrollmentParams.CourceStatus);
            _unitOfWork.Commit();
            return enrollmentParams;
        }

        public CourceModule SaveMatherial(CourceModule matherialParams)
        {
            _moduleRepository.Add(matherialParams);
            _unitOfWork.Commit();
            return matherialParams;
        }
    }
}