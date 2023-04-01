namespace DomainLab3.Models.Dtos
{
    public class CourceStatusDataDto
    {
        public Guid EnrollmentId { get; set; } = Guid.NewGuid();
        public List<ModuleStatusDataDto> Modules { get; set; } = new List<ModuleStatusDataDto>();
        public int Progress { get; set; } = 0;
        public int Duration { get; set; } = 0;
    }
}
