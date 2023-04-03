using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace DomainLab3
{
    [Table("cource_module")]
    public class CourceModule
    {
        public Guid EnrollmentId { get; set; } = Guid.Empty;
        public Guid ModuleId { get; set; } = Guid.Empty;

        [ForeignKey(nameof(EnrollmentId))]
        public CourceEnrollment Enrollment { get; set; }


        [ForeignKey(nameof(ModuleId))]
        public CourceMatherial Matherial { get; set; }

        public int Progress { get; set; } = 0;
        public int Duration { get; set; } = 0;
    }
}
