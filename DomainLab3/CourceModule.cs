using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DomainLab3
{
    [Table("cource_module")]
    public class CourceModule
    {
        public CourceModule() { }
        public CourceModule(bool isInitialize = false)
        {
            EnrollmentId = Guid.Empty;
            ModuleId = Guid.Empty;
            Progress = 0;
            Duration = 0;
        }

        [Key]
        public long Id { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid EnrollmentId { get; set; }
        public int EnrollmentKey { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid ModuleId { get; set; }

        [ForeignKey(nameof(EnrollmentId))]
        public CourceEnrollment Enrollment { get; set; }

        [ForeignKey(nameof(ModuleId))]
        public CourceMatherial Matherial { get; set; }

        public int Progress { get; set; }
        public int Duration { get; set; }
    }
}
