using System.ComponentModel.DataAnnotations.Schema;

namespace DomainLab3
{
    public class CourceEnrollment
    {
        public Guid EnrollmentId { get; set; } = Guid.Empty;
        public Guid CourceId { get; set; } = Guid.Empty;

        [ForeignKey(nameof(EnrollmentId))]
        public CourceStatus CourceStatus { get; set; }
        
        [ForeignKey(nameof(CourceId))]
        public Cource Cource { get; set; }
    }
}
