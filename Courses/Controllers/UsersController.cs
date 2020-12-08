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
using Microsoft.AspNetCore.Identity;

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
            _logger.LogInformation("Listando informações");
            return View(await _userRepository.PickLoggedUser(User));
        }

        [AllowAnonymous]
        public async Task<IActionResult> Register()
        {
            if (User.Identity.IsAuthenticated)
                await _userRepository.LogOut();

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
                DateTime date = DateTime.Now;
                string generatedMatricula = GenerateMatricula(date.Year, register.CPF);
                string cpf = RemoveNonNumeric(register.CPF);
                string telefone = RemoveNonNumeric(register.Telefone);

                User user = new User()
                {
                    Matricula = generatedMatricula,
                    UserName = generatedMatricula,
                    Nome = register.Nome,
                    PasswordHash = cpf,
                    CPF = cpf,
                    Telefone = telefone,
                    Email = register.Email,
                    DataCriacao = date
                    
                };

                _logger.LogInformation("Tentando criar usurário");
                var result = await _userRepository.SaveUser(user, cpf);

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

                    return RedirectToAction("Index", "Users");
                }

                else
                {
                    _logger.LogError("Erro ao criar o usuário");

                    foreach (var erro in result.Errors)
                    {
                        ModelState.AddModelError("", erro.Description.ToString());
                    }
                }
            }

            _logger.LogError("Informações de usuário inválidas");
            return View(register);
        }

        [HttpGet]
        public async Task<IActionResult> Update(string UserId)
        {
            _logger.LogInformation("Verificando se o usuário existe");
            var user = await _userRepository.PickById(UserId);

            var updateViewModel = new UpdateViewModel
            {
                Id = user.Id,
                DataCriacao = user.DataCriacao,
                DataAtualizacao = DateTime.Now,
                Nome = user.Nome,
                CPF = user.CPF,
                Email = user.Email,
                Telefone = user.Telefone

            };
            _logger.LogInformation("Atualizar usuário");
            return View(updateViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(UpdateViewModel updateViewModel)
        {
            if (ModelState.IsValid)
            {
                _logger.LogInformation("Pegando usuário pela matrícula");
                var user = await _userRepository.PickById(updateViewModel.Id);
                PasswordHasher<User> passwordHasher = new PasswordHasher<User>();
                if (passwordHasher.VerifyHashedPassword(user, user.PasswordHash, updateViewModel.Password) != PasswordVerificationResult.Failed)
                {
                    _logger.LogInformation("Informações corretas. Atualizando usuário");

                    user.Nome = updateViewModel.Nome;
                    user.Email = updateViewModel.Email;
                    user.Telefone = updateViewModel.Telefone;
                    user.DataAtualizacao = updateViewModel.DataAtualizacao;

                    //if

                    await _userRepository.UpdateUser(user);

                    return RedirectToAction("Index", "Users");
                }

                _logger.LogInformation("Informações inválidas");
                ModelState.AddModelError("", "Senha inválida");

            }
            _logger.LogError("Informações inválidas");

            return View(updateViewModel);
        }

        public async Task<IActionResult> LogOut()
        {
            await _userRepository.LogOut();

            return RedirectToAction("Index", "Home");
        }

        public string GenerateMatricula(int year, string cpf)
        {
            string matricula = (year.ToString()) + cpf[0] + cpf[1] + cpf[2];
            return matricula;
        }

        public static string RemoveNonNumeric(string text)
        {
            System.Text.RegularExpressions.Regex reg = new System.Text.RegularExpressions.Regex(@"[^0-9]");
            string ret = reg.Replace(text, string.Empty);
            return ret;
        }

        public async Task<IActionResult> Login()
        {
            if (User.Identity.IsAuthenticated)
            {
                await _userRepository.LogOut();
            }

            _logger.LogInformation("Entrando na página de login");
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel login)
        {
            if (ModelState.IsValid)
            {
                _logger.LogInformation("Pegando usuário pela matrícula");
                var user = await _userRepository.PickUserByMatricula(login.Matricula);
                PasswordHasher<User> passwordHasher = new PasswordHasher<User>();

                if(user != null)
                {
                    _logger.LogInformation("Verificando informações do usuário");
                    if(passwordHasher.VerifyHashedPassword(user, user.PasswordHash, login.Password) != PasswordVerificationResult.Failed)
                    {
                        _logger.LogInformation("Informações corretas. Logando usurário");
                        await _userRepository.Login(user, false);

                        return RedirectToAction("Index", "Users");
                    }

                    _logger.LogInformation("Informações inválidas");
                    ModelState.AddModelError("", "Matrícula e/ou senha inválidos");
                }

                _logger.LogInformation("Informações inválidas");
                ModelState.AddModelError("", "Matrícula e/ou senha inválidos");

            }

            return View(login);
        }
    }
}
