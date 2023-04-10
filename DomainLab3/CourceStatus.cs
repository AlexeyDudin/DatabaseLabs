using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DomainLab3
{
    [Table("cource_status")]
    public class CourceStatus
    {
        [Key]
        public Guid EnrollmentId { get; set; } = Guid.Empty;
        public List<CourceEnrollment> CourceEnrollments { get; set; }
        public int Progress { get; set; } = 0;
        public int Duration { get; set; } = 0;
    }
}
