using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Courses.AccessData.Interfaces;
using Courses.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Courses.Models;
using Microsoft.AspNetCore.Authorization;

namespace Courses.Controllers
{
    public class UsersController : Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly ILogger<UsersController> _logger;

        public UsersController(IUserRepository userRepository, ILogger<UsersController> logger)
        {
            _userRepository = userRepository;
            _logger = logger;
        }



        public async Task<IActionResult> Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                await _userRepository.Logout();
            }

            _logger.LogInformation("Entrando na página de registro");
            return View();
        }

        [AllowAnonymous]
        public async Task<IActionResult> Register()
        {
            if (User.Identity.IsAuthenticated)
                await _userRepository.Logout();

            _logger.LogInformation("Entrando na página de registro");
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel register)
        {
            if (ModelState.IsValid)
            {
                var user = new User
                {
                    UserName = register.NomeUser,
                    Email = register.Email,
                    CPF = register.CPF,
                    Telefone = register.Telefone,
                    Nome = register.Nome,
                    PasswordHash = register.CPF,
                };
                _logger.LogInformation("Tentando criar usurário");
                var result = await _userRepository.SaveUser(user, register.CPF);

                if (result.Succeeded)
                {
                    _logger.LogInformation("Novo usuário criado");
                    _logger.LogInformation("Atribuindo níveis de acesso ao novo usuário");
                    var accessLevel = "Aluno";

                    await _userRepository.AssignAccessLevel(user, accessLevel);
                    _logger.LogInformation("Atribuição concluída");

                    _logger.LogInformation("Logando usuário");
                    await _userRepository.Login(user, false);

                    _logger.LogInformation("Usuário logado com sucesso");

                    return RedirectToAction("Index", "Home");
                }

                else
                {
                    _logger.LogError("Erro ao criar o usuário");

                    foreach(var erro in result.Errors)
                    {
                        ModelState.AddModelError("", erro.Description.ToString());
                    }
                }
            }

            _logger.LogError("Informações de usuário inválidas");
            return View(register);
        }

    }
}
