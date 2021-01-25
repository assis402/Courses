using Courses.AccessData.Interfaces;
using Courses.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace Courses.ViewComponents
{
    public class UserMatriculationsViewComponent : ViewComponent
    {
        private readonly IUserRepository _userRepository;
        private readonly Context _context;

        public UserMatriculationsViewComponent(IUserRepository userRepository, Context context)
        {
            _userRepository = userRepository;
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync(string UserId)
        {
            User user = await _userRepository.GetLoggedUser(HttpContext.User);

            return View(await _context.Matriculation.Include(a => a.Course).Where(a => a.UserId == user.Id).ToListAsync());
        }
    }
}