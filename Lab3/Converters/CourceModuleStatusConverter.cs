using DomainLab3;
using DomainLab3.Models.Dtos;

namespace Lab3.Converters
{
    public static class CourceModuleStatusConverter
    {
        public static CourceModule ConvertToCourceModule(this SaveMatherialStatusParamsDto dto)
        {
            CourceModule courceModule = new CourceModule();
            courceModule.Progress = dto.Progress;
            courceModule.Duration = dto.SessionDuration;
            courceModule.EnrollmentId = dto.EnrollmentId;
            courceModule.ModuleId = dto.ModuleId;

            return courceModule;
        }

        public static SaveMatherialStatusParamsDto ConvertToMatherialStatusParamsDto(this CourceModule courceModule)
        {
            SaveMatherialStatusParamsDto result = new SaveMatherialStatusParamsDto();
            result.Progress = courceModule.Progress;
            result.SessionDuration = courceModule.Duration;
            result.EnrollmentId = courceModule.EnrollmentId;
            result.ModuleId = courceModule.ModuleId;

            return result;
        }
    }
}
