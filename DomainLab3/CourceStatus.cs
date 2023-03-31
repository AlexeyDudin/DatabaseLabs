namespace DomainLab3
{
    public class CourceStatus
    {
        public Guid EnrollmentId { get; set; } = Guid.Empty;
        public CourceEnrollment CourceEnrollment { get; set; }
        public int Progress { get; set; } = 0;
        public int Duration { get; set; } = 0;
    }
}
