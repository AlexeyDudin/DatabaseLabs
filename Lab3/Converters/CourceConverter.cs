using DomainLab3;
using DomainLab3.Models.Dtos;
using System.Runtime.CompilerServices;

namespace Lab3.Converters
{
    public static class CourceConverter
    {
        public static Cource ConvertToCource(this SaveCourceParamsDto saveCourceParamsDto)
        {
            Cource cource = new Cource()
            {
                Id = saveCourceParamsDto.CourceId,

                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                CourceMatherials = saveCourceParamsDto.ConvertToCourceMatherial(),
                CourceEnrollments = new List<CourceEnrollment>()
            };

            foreach (var matherial in cource.CourceMatherials)
            {
                matherial.Cource = cource;
                //matherial.CourceModule.Matherials.Add(matherial);
            }

            return cource;
        }

        public static SaveCourceParamsDto ConvertToCourceParamsDto(this Cource cource)
        {
            SaveCourceParamsDto saveCourceParamsDto = new()
            {
                CourceId = cource.Id,
                ModuleIds = cource.CourceMatherials.ConvertToMatherialsId(),
                RequiredModuleIds = cource.CourceMatherials.ConvertToRequiredMatherialsId(),
            };

            return saveCourceParamsDto;
        }
        public static CourceStatusDataDto ConvertToCourceStatusDataDto(this Cource cource, GetCourceStatusParamsDto param)
        {
            if (cource == null)
                return null;
            CourceStatusDataDto result = new CourceStatusDataDto();
            var enrollment = cource.CourceEnrollments.Where(e => e.EnrollmentId == param.EnrollmentId).FirstOrDefault();
            result.EnrollmentId = enrollment.EnrollmentId;
            result.Duration = enrollment.CourceStatus.Duration;
            result.Progress = enrollment.CourceStatus.Progress;
            result.Modules = cource.CourceMatherials.ConvertToModuleStatusDataDtoList();
            return result;
        }
    }
}
