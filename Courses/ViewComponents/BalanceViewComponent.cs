using Courses.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Courses.ViewComponents
{
    public class BalanceViewComponent : ViewComponent
    {
        private readonly Context _context;

        public BalanceViewComponent(Context context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync(string UserId)
        {
            return View(await _context.Wallets.FirstOrDefaultAsync(c => c.UserId == UserId));
        }

    }
}
