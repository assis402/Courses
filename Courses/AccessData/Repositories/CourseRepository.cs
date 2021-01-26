using Courses.AccessData.Interfaces;
using Courses.Models;
using System.Linq;

namespace Courses.AccessData.Repositories
{
    public class CourseRepository : GenericRepository<Course>, ICourseRepository
    {
        private readonly Context _context;

        public CourseRepository(Context context) : base(context)
        {
            _context = context;
        }

        public double GetPriceById(string Id)
        {
            return _context.Courses.FirstOrDefault(c => c.CourseId == Id).Price;
        }
    }
}
