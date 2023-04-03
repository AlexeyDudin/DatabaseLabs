using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DomainLab3
{
    [Table("cource")]
    public class Cource
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();
        public int Version { get; set; } = 0;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
        public List<CourceMatherial> CourceMatherials { get; set; } = new ();
        public List<CourceEnrollment> CourceEnrollments { get; set; } = new();
    }
}
