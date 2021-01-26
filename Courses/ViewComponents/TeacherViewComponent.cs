using Courses.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Courses.ViewComponents
{
    public class TeacherViewComponent : ViewComponent
    {
        private readonly Context _context;

        public TeacherViewComponent(Context context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync(string UserId)
        {
            return View(await _context.Users.FirstOrDefaultAsync(c => c.UserId == UserId));
        }

    }
}
