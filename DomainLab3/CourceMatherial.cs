using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DomainLab3
{
    [Table("cource_matherial")]
    public class CourceMatherial
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid ModuleId { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid CourceId { get; set; }

        public long CourceModuleId { get; set; }

        public CourceMatherial()
        { }

        public CourceMatherial(bool isInitialize = false)
        {
            if (isInitialize)
            {
                ModuleId = Guid.Empty;
                CourceId = Guid.Empty;
                IsRequired = false;
                CreatedAt = DateTime.UtcNow;
                UpdatedAt = DateTime.UtcNow;
            }
        }

        [ForeignKey(nameof(CourceId))]
        public Cource Cource { get; set; }

        public CourceModule CourceModule { get; set; }

        public bool IsRequired { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
