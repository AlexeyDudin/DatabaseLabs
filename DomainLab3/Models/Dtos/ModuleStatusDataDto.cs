namespace DomainLab3.Models.Dtos
{
    public class ModuleStatusDataDto
    {
        public Guid ModuleId { get; set; } = Guid.NewGuid();
        public int Progress { get; set; } = 0;
    }
}
