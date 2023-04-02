using DomainLab3;
using DomainLab3.Models.Dtos;

namespace Infrastructure.Repository
{
    public interface ICourceRepository
    {
        Cource SaveCourse(Cource saveCourseParams);
        Cource GetCourceById(Guid courceId);
        void DeleteCource(Guid courceId);
    }
}
