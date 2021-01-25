using Courses.AccessData.Interfaces;
using Courses.Models;
using System.Linq;

namespace Courses.AccessData.Repositories
{
    public class FeatureRepository : GenericRepository<Feature>, IFeatureRepository
    {
        private readonly Context _context;

        public FeatureRepository(Context context) : base(context)
        {
            _context = context;
        }

        public IQueryable<Feature> GetAllByStatus(FeatureStatus featureStatus)
        {
            return _context.Features.Where(x => x.Status == featureStatus);
        }

        public IQueryable<Feature> GetByStatus(FeatureStatus featureStatus, int listSize)
        {
            return _context.Features.Where(x => x.Status == featureStatus).OrderByDescending(f => f.FeatureId).Take(listSize);
        }
    }
}
