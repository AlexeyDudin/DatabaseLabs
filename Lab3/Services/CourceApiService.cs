using Application;
using Application.Models.Dtos;
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
                return new Responce(_courceService.DeleteCource(courceId), ResponceCode.Ok);
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
                return new Responce(_courceService.GetCourceStatus(matherialParams), ResponceCode.Ok);
            }
            catch (Exception ex)
            {
                return new Responce(ex.Message, ResponceCode.Error);
            }
        }

        public Responce SaveCource(SaveCourceParamsDto courceParams)
        {
            try
            {
                return new Responce(_courceService.SaveCource(courceParams), ResponceCode.Ok);
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
                return new Responce(_courceService.SaveEnrollment(enrollmentParams), ResponceCode.Ok);
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
