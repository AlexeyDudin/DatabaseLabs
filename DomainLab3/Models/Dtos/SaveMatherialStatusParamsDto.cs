namespace DomainLab3.Models.Dtos
{
    public class SaveMatherialStatusParamsDto
    {
        public Guid EnrollmentId { get; set; } = Guid.Empty;
        public Guid ModuleId { get; set; } = Guid.Empty;
        public int Progress { get; set; } = 0;
        public int SessionDuration { get; set; } = 0;
    }
}
