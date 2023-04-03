using DomainLab3;
using Infrastructure.DbWorkers.Common;

namespace Infrastructure.Converters
{
    public static class DbDtoConverter
    {
        public static Cource ConvertToCource(this DbDto dto)
        {
            if (dto == null || dto.Get().Count == 0)
                return null;
            Cource result = new Cource();
            result.Id = Guid.Parse(dto.Get()[0][0]);
            result.Version = Int32.Parse(dto.Get()[0][1]);
            result.CreatedAt = DateTime.Parse(dto.Get()[0][2]);
            if (!string.IsNullOrEmpty((dto.Get()[0][3])))
                result.UpdatedAt = DateTime.Parse((dto.Get()[0][3]));
            if (!string.IsNullOrEmpty((dto.Get()[0][4])))
                return null;
            return result;
        }

        public static List<CourceMatherial> ConvertToMatherials(this DbDto dto, Cource cource = null)
        {
            if (dto == null || dto.Get().Count == 0)
                return null;
            List<CourceMatherial> result = new List<CourceMatherial>();
            foreach (var line in dto.Get())
            {
                CourceMatherial courceMatherial = new CourceMatherial();
                courceMatherial.ModuleId = Guid.Parse(line[0]);
                courceMatherial.CourceId = Guid.Parse(line[1]);
                courceMatherial.IsRequired = Boolean.Parse(line[2]);
                if (!string.IsNullOrWhiteSpace(line[3]))
                    courceMatherial.CreatedAt = DateTime.Parse(line[3]);
                if (!string.IsNullOrWhiteSpace(line[4]))
                    courceMatherial.UpdatedAt = DateTime.Parse(line[4]);
                courceMatherial.Cource = cource;
                if (!string.IsNullOrEmpty(line[5]))
                    continue;
                result.Add(courceMatherial);
            }
            return result;
        }

        public static CourceMatherial ConvertToMatherials(this DbDto dto, CourceModule module)
        {
            if (dto == null || dto.Get().Count == 0)
                return null;

            CourceMatherial courceMatherial = new CourceMatherial();
            foreach (var line in dto.Get())
            {
                courceMatherial.ModuleId = Guid.Parse(line[0]);
                courceMatherial.CourceId = Guid.Parse(line[1]);
                courceMatherial.IsRequired = Boolean.Parse(line[2]);
                if (!string.IsNullOrWhiteSpace(line[3]))
                    courceMatherial.CreatedAt = DateTime.Parse(line[3]);
                if (!string.IsNullOrWhiteSpace(line[4]))
                    courceMatherial.UpdatedAt = DateTime.Parse(line[4]);
                courceMatherial.CourceModule = module;
                module.Matherial = courceMatherial;
                if (!string.IsNullOrEmpty(line[5]))
                    continue;
            }
            return courceMatherial;
        }

        public static CourceModule ConvertToModule(this DbDto dto, Cource cource = null)
        {
            if (dto == null || dto.Get().Count == 0)
                return null;
            CourceModule result = new CourceModule();
            result.EnrollmentId = Guid.Parse(dto.Get()[0][0]);
            result.ModuleId = Guid.Parse(dto.Get()[0][1]);
            result.Progress = Int32.Parse(dto.Get()[0][2]);
            result.Duration = Int32.Parse(dto.Get()[0][3]);
            if (!string.IsNullOrEmpty(dto.Get()[0][4]))
                return null;
            if (cource != null)
            {
                var matherial = cource.CourceMatherials.Where(m => m.ModuleId == result.ModuleId).SingleOrDefault();
                result.Matherial = matherial;
                matherial.CourceModule = result;
            }
            return result;
        }

        public static List<CourceEnrollment> ConvertToEnrollments(this DbDto dto, Cource cource)
        {
            if (dto == null || dto.Get().Count == 0)
                return null;
            List<CourceEnrollment> result = new List<CourceEnrollment>();
            foreach (var line in dto.Get())
            {
                CourceEnrollment courceEnrollment = new CourceEnrollment();
                courceEnrollment.EnrollmentId = Guid.Parse(line[0]);
                courceEnrollment.CourceId = Guid.Parse(line[1]);
                courceEnrollment.Cource = cource;
                result.Add(courceEnrollment);
            }

            foreach (var module in cource.CourceMatherials)
            {
                var enrollment = cource.CourceEnrollments.Where(e => e.EnrollmentId == module.CourceModule.EnrollmentId).FirstOrDefault();
                if (module.CourceModule != null)
                    module.CourceModule.Enrollment = enrollment;
                if (enrollment != null)
                    enrollment.CourceModule = module.CourceModule;
            }

            return result;
        }

        public static CourceStatus ConvertToStatus(this DbDto dto, Cource cource)
        {
            if (dto == null || dto.Get().Count == 0)
                return null;
            CourceStatus result = new CourceStatus();
            result.EnrollmentId = Guid.Parse(dto.Get()[0][0]);
            result.Progress = Int32.Parse(dto.Get()[0][1]);
            result.Duration = Int32.Parse(dto.Get()[0][2]);
            if (!string.IsNullOrEmpty(dto.Get()[0][3]))
                return null;

            var enr = cource.CourceEnrollments.Where(ce => ce.EnrollmentId == result.EnrollmentId).SingleOrDefault();
            result.CourceEnrollment = enr;
            enr.CourceStatus = result;
            return result;
        }
    }
}
