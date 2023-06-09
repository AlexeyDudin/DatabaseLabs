﻿using DomainLab3;
using DomainLab3.Models.Dtos;

namespace Lab3.Converters
{
    public static class CourceMatherialConverter
    {
        public static CourceMatherial GetNewCourceMatherial(this Guid guid, bool isRequired)
        {
            CourceMatherial newCourceMatherial = new CourceMatherial(true);
            newCourceMatherial.ModuleId = guid;
            newCourceMatherial.IsRequired = isRequired;
            //newCourceMatherial.CourceModule = null; //guid.ConvertToCourceModuleStatus(newCourceMatherial);
            return newCourceMatherial;
        }

        public static List<CourceMatherial> ConvertToCourceMatherial(this SaveCourceParamsDto courceDataDto)
        {
            List<CourceMatherial> courceMatherials = new ();

            foreach (var module in courceDataDto.RequiredModuleIds)
            {
                courceMatherials.Add(module.GetNewCourceMatherial(true));
            }

            foreach (var module in courceDataDto.ModuleIds)
            {
                if (!courceDataDto.RequiredModuleIds.Contains(module)) // Без повторов
                    courceMatherials.Add(module.GetNewCourceMatherial(false));
            }

            return courceMatherials;
        }

        public static List<Guid> ConvertToMatherialsId(this List<CourceMatherial> courceMatherials)
        {
            List<Guid> result = new ();
            foreach (var courceMatherial in courceMatherials)
            {
                result.Add(courceMatherial.ModuleId);
            }
            return result;
        }

        public static List<Guid> ConvertToRequiredMatherialsId(this List<CourceMatherial> courceMatherials)
        {
            List<Guid> result = new ();
            foreach (var courceMatherial in courceMatherials)
            {
                if (courceMatherial.IsRequired)
                    result.Add(courceMatherial.ModuleId);
            }
            return result;
        }

        public static List<ModuleStatusDataDto> ConvertToModuleStatusDataDtoList(this List<CourceMatherial> courceMatherials)
        {
            List<ModuleStatusDataDto> result = new List<ModuleStatusDataDto>();

            foreach (var matherial in courceMatherials)
            {
                ModuleStatusDataDto moduleStatusDataDto = new ModuleStatusDataDto();
                moduleStatusDataDto.ModuleId = matherial.ModuleId;
                result.Add(moduleStatusDataDto);
            }

            return result;
        }
    }
}
