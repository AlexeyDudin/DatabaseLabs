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
                CourceMatherials = saveCourceParamsDto.ConvertToCourceMatherial()
            };
            
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
    }
}
