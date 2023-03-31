using System.Collections.ObjectModel;

namespace DomainLab3
{
    public class CourceModuleStatus
    {
        public Guid EnrollmentId { get; set; } = Guid.Empty;
        public CourceEnrollment CourceEnrollment { get; set; }
        public Guid ModuleId { get; set; } = Guid.Empty;
        public CourceMatherial CourceMatherial { get; set; }
        public int Progress { get; set; } = 0;
        public int Duration { get; set; } = 0;
    }
}
