using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DomainLab4
{
    [Table("cource")]
    public class Cource
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();
        public int Version { get; set; } = 0;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
        public List<CourceMatherial> Materials { get; set; } = new ();
    }
}