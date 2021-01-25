using Courses.Models;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Courses.AccessData.Interfaces
{
    public interface IUserRepository : IGenericRepository<User>
    {
        Task<User> GetLoggedUser(ClaimsPrincipal user);
        Task<IdentityResult> SaveUser(User user, string password);
        Task UpdateUser(User user);
        Task AssignAccessLevel(User user, string accessLevel);
        Task Login(User user, bool remember);
        Task LogOut();
        Task<User> GetUserByMatricula(string matricula);

    }
}
