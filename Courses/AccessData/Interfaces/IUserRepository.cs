using Courses.AccessData.Repositories;
using Courses.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Courses.AccessData.Interfaces
{
    public interface IUserRepository : IGenericRepository<User>
    {
        Task<User> PickLoggedUser(ClaimsPrincipal usuario);
        Task<IdentityResult> SaveUser(User user, string password);
        Task UpdateUser(User user);
        Task AssignAccessLevel(User user, string accessLevel);
        Task Login(User user, bool remember);
        Task Logout();
        Task<User> PickUserByEmail(string email);

    }
}
