using System.ComponentModel.DataAnnotations.Schema;

namespace DomainLab4
{
    public class CourceStatus
    {
        public Guid EnrollmentId { get; set; } = Guid.Empty;
        public int Progresss { get; set; } = 0;
        public int Duration { get; set; } = 0;

        [ForeignKey(nameof(EnrollmentId))]
        public Enrollment Enrollment { get; set; }
    }
}
