using Courses.Models;
using System.Linq;

namespace Courses.AccessData.Interfaces
{
    public interface IUpgradeRepository : IGenericRepository<Upgrade>
    {
        IQueryable<Upgrade> GetLastFive();
    }
}
