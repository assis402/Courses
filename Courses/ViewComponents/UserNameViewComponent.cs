using Courses.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace Courses.ViewComponents
{
    public class UserNameViewComponent : ViewComponent
    {
        private readonly Context _context;

        public UserNameViewComponent(Context context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync(string UserId)
        {
            string userName = _context.Users.FirstOrDefault(c => c.Id == UserId).UserName;
            return View("Default",userName);
        }
    }
}