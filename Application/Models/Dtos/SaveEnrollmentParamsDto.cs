namespace Application.Models.Dtos
{
    public class SaveEnrollmentParamsDto
    {
        public Guid EnrollmentId { get; set; } = Guid.Empty;
        public Guid CourceId { get; set; } = Guid.Empty;
    }
}
