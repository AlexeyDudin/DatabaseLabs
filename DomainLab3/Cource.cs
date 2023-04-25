using Microsoft.EntityFrameworkCore.Infrastructure;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DomainLab3
{
    [Table("cource")]
    public class Cource
    {
        private List<CourceMatherial> courceMatherials= new List<CourceMatherial>();
        private List<CourceEnrollment> courceEnrollments = new List<CourceEnrollment>();

        public Cource()
        { }

        private Cource(ILazyLoader lazyLoader)
        {
            LazyLoader = lazyLoader;
        }

        private ILazyLoader LazyLoader { get; set; }

        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();
        public int Version { get; set; } = 0;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
        public List<CourceMatherial> CourceMatherials { get => LazyLoader.Load(this, ref courceMatherials); set => courceMatherials = value; }
        public List<CourceEnrollment> CourceEnrollments { get => LazyLoader.Load(this, ref courceEnrollments); set => courceEnrollments = value; }
    }
}
