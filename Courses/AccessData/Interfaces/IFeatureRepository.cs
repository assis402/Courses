using Courses.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Courses.AccessData.Interfaces
{
    public interface IFeatureRepository : IGenericRepository<Feature>
    {
        IQueryable<Feature> GetAllByStatus(FeatureStatus featureStatus);
        IQueryable<Feature> GetByStatus(FeatureStatus featureStatus, int listSize);
    }
}
