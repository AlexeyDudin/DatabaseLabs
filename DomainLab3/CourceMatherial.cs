namespace DomainLab3
{
    public class CourceMatherial
    {
        public Guid ModuleId { get; set; } = Guid.Empty;
        public Cource Cource { get; set; }
        public Guid CourceId { get; set; }
        public bool Isrequired { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    }
}
