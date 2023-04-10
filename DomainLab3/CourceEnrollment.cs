using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DomainLab3
{
    [Table("cource_enrollment")]
    public class CourceEnrollment
    {
        [Key]
        public int Id { get; set; }
        public Guid EnrollmentId { get; set; } = Guid.Empty;
        public Guid CourceId { get; set; } = Guid.Empty;

        [ForeignKey(nameof(EnrollmentId))]
        public CourceStatus CourceStatus { get; set; }

        public CourceModule CourceModule { get; set; }
        
        [ForeignKey(nameof(CourceId))]
        public Cource Cource { get; set; }
    }
}
