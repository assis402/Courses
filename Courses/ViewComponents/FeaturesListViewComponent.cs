using Courses.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Courses.ViewComponents
{
    public class FeaturesListViewComponent : ViewComponent
    {
        private readonly Context _context;

        public FeaturesListViewComponent(Context context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync(string UpgradeId)
        {
            return View(await _context.Features.AnyAsync(f => f.UpgradeId == UpgradeId));
        }

    }
}
