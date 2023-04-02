using System.ComponentModel.DataAnnotations.Schema;

namespace DomainLab3
{
    public class CourceMatherial
    {
        public Guid ModuleId { get; set; } = Guid.Empty;
        public Guid CourceId { get; set; } = Guid.Empty;

        [ForeignKey(nameof(CourceId))]
        public Cource Cource { get; set; }

        [ForeignKey(nameof(ModuleId))]
        public CourceModule CourceModule { get; set; }

        public bool IsRequired { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    }
}
