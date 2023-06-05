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

        public Cource(bool isInitialize = false)
        {
            if (isInitialize)
            {
                Id = Guid.NewGuid();
                Version = 0;
                CreatedAt = DateTime.UtcNow;
                UpdatedAt = DateTime.UtcNow;
            }
        }

        private Cource(ILazyLoader lazyLoader)
        {
            LazyLoader = lazyLoader;
        }

        private ILazyLoader LazyLoader { get; set; }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid Id { get; set; }
        public int Version { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public List<CourceMatherial> CourceMatherials { get => LazyLoader.Load(this, ref courceMatherials); set => courceMatherials = value; }
        public List<CourceEnrollment> CourceEnrollments { get => LazyLoader.Load(this, ref courceEnrollments); set => courceEnrollments = value; }
    }
}
