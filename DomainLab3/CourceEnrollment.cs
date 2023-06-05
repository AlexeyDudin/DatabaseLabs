using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DomainLab3
{
    [Table("cource_enrollment")]
    public class CourceEnrollment
    {
        public CourceEnrollment() { }

        public CourceEnrollment(bool isInitialize = false)
        {
            if (isInitialize)
            {
                EnrollmentId = Guid.Empty;
                CourceId = Guid.Empty;
            }
        }

        [Key]
        public int Id { get; set; }
        public long CourceModuleId { get; set; }
        public Guid EnrollmentId { get; set; }
        public Guid CourceId { get; set; }

        [ForeignKey(nameof(EnrollmentId))]
        public CourceStatus CourceStatus { get; set; }

        [ForeignKey(nameof(CourceModuleId))]
        public CourceModule CourceModule { get; set; }
        
        [ForeignKey(nameof(CourceId))]
        public Cource Cource { get; set; }
    }
}
