using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DomainLab3
{
    [Table("cource_status")]
    public class CourceStatus
    {
        public CourceStatus() { }
        public CourceStatus(bool isInitialize = false)
        {
            if (isInitialize)
            {
                EnrollmentId = Guid.Empty;
                Progress = 0;
                Duration = 0;
            }
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid EnrollmentId { get; set; }
        public List<CourceEnrollment> CourceEnrollments { get; set; } = new List<CourceEnrollment>();
        public int Progress { get; set; }
        public int Duration { get; set; }
    }
}
