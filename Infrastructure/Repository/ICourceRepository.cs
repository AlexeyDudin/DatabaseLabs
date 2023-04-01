using DomainLab3.Models.Dtos;

namespace Infrastructure.Repository
{
    public interface ICourceRepository
    {
        SaveCourceParamsDto SaveCourse(SaveCourceParamsDto saveCourseParams);
    }
}
