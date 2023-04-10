using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DomainLab3
{
    [Table("cource_module")]
    public class CourceModule
    {
        [Key]
        //public int Id { get; set; }
        public Guid EnrollmentId { get; set; } = Guid.Empty;
        public Guid ModuleId { get; set; } = Guid.Empty;

        public List<CourceEnrollment> Enrollments { get; set; } = new List<CourceEnrollment>();

        public List<CourceMatherial> Matherials { get; set; } = new List<CourceMatherial>();

        public int Progress { get; set; } = 0;
        public int Duration { get; set; } = 0;
    }
}
