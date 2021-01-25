using System.Linq;
using System.Threading.Tasks;

namespace Courses.AccessData.Interfaces
{
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        IQueryable<TEntity> GetAll();
        Task<TEntity> GetById(int id);
        Task<TEntity> GetById(string id);
        Task Insert(TEntity entity);
        Task Update(TEntity entity);
        Task Delete(int id);
        Task Delete(string id);
    }
}
