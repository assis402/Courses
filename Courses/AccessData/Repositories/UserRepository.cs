﻿using Courses.AccessData.Interfaces;
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
        private readonly SignInManager<User> _loginManager;
        private readonly UserManager<User> _userManager;

        public UserRepository(Context context, SignInManager<User> loginManager, UserManager<User> userManager) : base(context)
        {
            _loginManager = loginManager;
            _userManager = userManager;
        }

        public async Task<User> PickLoggedUser(ClaimsPrincipal user)
        {
            return await _userManager.GetUserAsync(user);
        }

        public async Task<IdentityResult> SaveUser(User user, string password)
        {
            return await _userManager.CreateAsync(user, password);
        }

        public async Task AssignAccessLevel(User user, string accessLevel)
        {
            await _userManager.AddToRoleAsync(user, accessLevel);
        }

        public async Task Login(User user, bool remember)
        {
            await _loginManager.SignInAsync(user, remember);
        }

        public async Task LogOut()
        {
            await _loginManager.SignOutAsync();
        }

        public async Task<User> PickUserByMatricula(string matricula)
        {
            return await _userManager.FindByNameAsync(matricula);
        }

        public async Task UpdateUser(User user)
        {
            await _userManager.UpdateAsync(user);
        }
    }
}
