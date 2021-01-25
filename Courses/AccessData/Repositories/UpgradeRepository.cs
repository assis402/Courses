using Courses.AccessData.Interfaces;
using Courses.Models;
using System.Linq;

namespace Courses.AccessData.Repositories
{
    public class UpgradeRepository : GenericRepository<Upgrade>, IUpgradeRepository
    {

        private readonly Context _context;

        public UpgradeRepository(Context context) : base(context)
        {
            _context = context;
        }

        public IQueryable<Upgrade> GetLastFive()
        {
            return _context.Upgrades.OrderByDescending(u => u.UpgradeId).Take(5);
        }
    }
}
