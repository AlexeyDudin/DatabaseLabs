using Application;
using DomainLab3;
using DomainLab3.Models.Dtos;
using Lab3.Converters;
using Lab3.ResponceWorker;

namespace Lab3
{
    public class CourceApiService : ICourceApiService
    {
        private readonly ICourceService _courceService;
        public CourceApiService(ICourceService courceService)
        {
            _courceService = courceService;
        }

        public Responce DeleteCource(Guid courceId)
        {
            try
            {
                return new Responce(_courceService.DeleteCource(courceId).ConvertToCourceParamsDto(), ResponceCode.Ok);
            }
            catch (Exception ex)
            {
                return new Responce(ex.Message, ResponceCode.Error);
            }
        }

        public Responce GetCourceStatus(GetCourceStatusParamsDto matherialParams)
        {
            try
            {
                return new Responce(_courceService.GetCourceStatus(matherialParams).ConvertToCourceStatusDataDto(matherialParams), ResponceCode.Ok);
            }
            catch (Exception ex)
            {
                return new Responce(ex.Message, ResponceCode.Error);
            }
        }

        public Responce SaveCource(Cource cource)
        {
            try
            {
                return new Responce(_courceService.SaveCource(cource).ConvertToCourceParamsDto(), ResponceCode.Ok);
            }
            catch (Exception ex)
            {
                return new Responce(ex.Message, ResponceCode.Error);
            }
        }

        public Responce SaveEnrollment(SaveEnrollmentParamsDto enrollmentParams)
        {
            try
            {
                return new Responce(_courceService.SaveEnrollment(enrollmentParams.ConvertToCourceEnrollment()).ConvertToSaveEnrollmentParamsDto(), ResponceCode.Ok);
            }
            catch (Exception ex)
            {
                return new Responce(ex.Message, ResponceCode.Error);
            }
        }

        public Responce SaveMatherial(SaveMatherialStatusParamsDto matherialParams)
        {
            try
            {
                return new Responce(_courceService.SaveMatherial(matherialParams), ResponceCode.Ok);
            }
            catch (Exception ex)
            {
                return new Responce(ex.Message, ResponceCode.Error);
            }
        }
    }
}
