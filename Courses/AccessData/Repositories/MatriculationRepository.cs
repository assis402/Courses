using Courses.AccessData.Interfaces;
using Courses.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Courses.AccessData.Repositories
{
    public class MatriculationRepository : GenericRepository<Matriculation>, IMatriculationRepository
    {
        private readonly Context _context;

        public MatriculationRepository(Context context) : base(context)
        {
            _context = context;

        }

        public async Task<bool> AlreadyMatriculate(string userId, string courseId)
        {
            return await _context.Matriculations.AnyAsync(a => a.UserId == userId && a.CourseId == courseId);
        }
    }
}
