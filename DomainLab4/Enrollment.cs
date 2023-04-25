using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DomainLab4
{
    [Table("cource_enrollment")]
    public class Enrollment
    {
        [Key]
        public int Id { get; set; }
        public Guid EnrollmentId { get; set; } = Guid.Empty;
        public Guid CourceId { get; set; } = Guid.Empty;

        [ForeignKey(nameof(CourceId))]
        public Cource Cource { get; set; }

        public CourceStatus Status { get; set; }
    }
}
