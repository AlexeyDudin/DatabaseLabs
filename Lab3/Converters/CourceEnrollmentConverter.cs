using DomainLab3;
using DomainLab3.Models.Dtos;

namespace Lab3.Converters
{
    public static class CourceEnrollmentConverter
    {
        public static CourceEnrollment ConvertToCourceEnrollment(this SaveEnrollmentParamsDto saveEnrollmentParamsDto)
        {
            CourceEnrollment result = new CourceEnrollment();
            result.EnrollmentId = saveEnrollmentParamsDto.EnrollmentId;
            result.CourceId = saveEnrollmentParamsDto.CourceId;
            result.CourceStatus = new CourceStatus();
            result.CourceStatus.CourceEnrollments.Add(result);
            result.CourceStatus.EnrollmentId = saveEnrollmentParamsDto.EnrollmentId;
            return result;
        }

        public static SaveEnrollmentParamsDto ConvertToSaveEnrollmentParamsDto(this CourceEnrollment courceEnrollment)
        {
            SaveEnrollmentParamsDto result = new SaveEnrollmentParamsDto();
            result.EnrollmentId = courceEnrollment.EnrollmentId;
            result.CourceId = courceEnrollment.CourceId;
            return result;
        }
    }
}
