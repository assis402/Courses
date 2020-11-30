using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Courses.AccessData.Interfaces;
using Courses.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Courses.Controllers
{
    public class UsersController : Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly ILogger _logger;

        public UsersController(IUserRepository userRepository, ILogger logger)
        {
            _userRepository = userRepository;
            _logger = logger;
        }



      /* public async IActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                await _userRepository.Logout();
            }

            _logger.LogInformation("Entrando na página de registro");
            return View();
        }*/

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        /*public async Task<IActionResult> Register(RegisterViewModel register)
        {
            if (ModelState.IsValid)
            {
                var user = new UsersController
                {
                    UserName = register.NomeUser,
                    Email = 
                }
            }
        }*/
    }
}
