using Courses.AccessData.Interfaces;
using Courses.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Courses.AccessData.Repositories
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        private readonly SignInManager<User> _loginManeger;
        private readonly UserManager<User> _userManeger;

        public UserRepository(Context context, SignInManager<User> loginManeger, UserManager<User> userManeger) : base(context)
        {
            _loginManeger = loginManeger;
            _userManeger = userManeger;
        }

        public async Task<User> PickLoggedUser(ClaimsPrincipal user)
        {
            return await _userManeger.GetUserAsync(user);
        }

        public async Task<IdentityResult> SaveUser(User user, string password)
        {
            return await _userManeger.CreateAsync(user, password);
        }

        public async Task AssignAccessLevel(User user, string accessLevel)
        {
            await _userManeger.AddToRoleAsync(user, accessLevel);
        }

        public async Task Login(User user, bool remember)
        {
            await _loginManeger.SignInAsync(user, remember);
        }

        public async Task Logout()
        {
            await _loginManeger.SignOutAsync();
        }

        public async Task<User> PickUserByEmail(string email)
        {
            return await _userManeger.FindByEmailAsync(email);
        }

        public async Task UpdateUser(User user)
        {
            await _userManeger.UpdateAsync(user);
        }
    }
}
