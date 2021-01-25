using Courses.Models;

namespace Courses.AccessData.Interfaces
{
    public interface ICourseRepository : IGenericRepository<Course>
    {
        double GetPriceById(string Id);
    }
}
