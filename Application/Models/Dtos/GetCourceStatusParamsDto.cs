namespace Application.Models.Dtos
{
    public class GetCourceStatusParamsDto
    {
        public Guid EnrollmentId { get; set; } = Guid.Empty;
        public Guid CourceId { get; set; } = Guid.Empty;
    }
}
