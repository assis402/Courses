using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Courses.AccessData.Interfaces
{
    interface IGenericRepository<TEntity> where TEntity : class
    {
        IQueryable<TEntity> PickAll();
        Task<TEntity> PickById(int id);
        Task<TEntity> PickById(string id);
        Task Insert(TEntity entity);
        Task Update(TEntity entity);
        Task Delete(int id);
        Task Delete(string id);
    }
}
