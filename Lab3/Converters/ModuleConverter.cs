﻿using DomainLab3;
using DomainLab3.Models.Dtos;

namespace Lab3.Converters
{
    public static class ModuleConverter
    {
        public static CourceModule ConvertToCourceModuleStatus(this Guid moduleGuid, CourceMatherial matherial) 
        {
            CourceModule module = new CourceModule();
            module.ModuleId = moduleGuid;
            module.EnrollmentId = Guid.Empty;
            module.Enrollments = null;
            module.Progress = 0;
            module.Duration = 0;
            module.Matherials.Add(matherial);

            return module;
        }
    }
}
