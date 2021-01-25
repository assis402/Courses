using System.Threading.Tasks;
using Courses.Models;

namespace Courses.AccessData.Interfaces
{
    public interface IAccessLevelsRepository : IGenericRepository<AccessLevels>
    {
        Task<bool> AccessLevelExists(string accessLevel);

        new Task Update(AccessLevels accessLevel);
    }
}
