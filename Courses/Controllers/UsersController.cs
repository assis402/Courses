using System;
using System.Threading.Tasks;
using Courses.AccessData.Interfaces;
using Courses.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Courses.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System.IO;

namespace Courses.Controllers
{
    public class UsersController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly IUserRepository _userRepository;
        readonly IHostingEnvironment _hostingEnviroment;
        private readonly IWalletRepository _walletRepository;
        private readonly ILogger<UsersController> _logger;

        public UsersController(UserManager<User> userManager, IUserRepository userRepository, IHostingEnvironment hostingEnviroment, IWalletRepository walletRepository, ILogger<UsersController> logger)
        {
            _userManager = userManager;
            _userRepository = userRepository;
            _hostingEnviroment = hostingEnviroment;
            _walletRepository = walletRepository;
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            _logger.LogInformation("Listando informações");
            return View(await _userRepository.GetLoggedUser(User));
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
                string phoneNumber = RemoveNonNumeric(register.PhoneNumber);

                User user = new User()
                {
                    Matricula = generatedMatricula,
                    Name = register.Name,
                    UserName = register.Nickname,
                    PasswordHash = cpf,
                    CPF = cpf,
                    PhoneNumber = phoneNumber,
                    Email = register.Email,
                    CreationDate = date,
                    UpdateDate = date
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

                    var wallet = new Wallet() { Balance = 0, User = user, UserId = user.Id };
                    user.Wallet = wallet;

                    await _walletRepository.Insert(wallet);

                    _logger.LogInformation("Carteira atribuída");

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
            var user = await _userRepository.GetById(UserId);

            var updateViewModel = new UpdateViewModel
            {
                Id = user.Id,
                CreationDate = user.CreationDate,
                UpdateDate = DateTime.Now,
                Name = user.Name,
                CPF = user.CPF,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber
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
                var user = await _userRepository.GetById(updateViewModel.Id);
                PasswordHasher<User> passwordHasher = new PasswordHasher<User>();
                if (passwordHasher.VerifyHashedPassword(user, user.PasswordHash, updateViewModel.Password) != PasswordVerificationResult.Failed)
                {
                    _logger.LogInformation("Informações corretas. Atualizando usuário");

                    user.Name = updateViewModel.Name;
                    user.Email = updateViewModel.Email;
                    user.PhoneNumber = updateViewModel.PhoneNumber;
                    user.UpdateDate = updateViewModel.UpdateDate;

                    await _userRepository.UpdateUser(user);

                    return RedirectToAction("Index", "Users");
                }

                _logger.LogInformation("Informações inválidas");
                ModelState.AddModelError("", "Senha inválida");

            }
            _logger.LogError("Informações inválidas");

            return View(updateViewModel);
        }

        [HttpGet]
        public async Task<IActionResult> Promote(string UserId)
        {
            _logger.LogInformation("Verificando se o usuário existe");
            var user = await _userRepository.GetById(UserId);
            var accessLevel = await _userManager.GetRolesAsync(user);
            var promoteViewModel = new PromoteViewModel
            {
                Id = user.Id,
                AccessLevel = accessLevel[0],
                UpdateDate = DateTime.Now
            };

            _logger.LogInformation("Promovendo usuário");
            return View(promoteViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Promote(PromoteViewModel promoteViewModel)
        {
            if (ModelState.IsValid)
            {
                _logger.LogInformation("Pegando usuário pela matrícula");

                var user = await _userRepository.GetById(promoteViewModel.Id);

                await _userRepository.AssignAccessLevel(user, promoteViewModel.AccessLevel);
                _logger.LogInformation("Atribuição concluída");

                return RedirectToAction("Index", "Users");
            }
            _logger.LogError("Informações inválidas");

            return View(promoteViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangePhoto(string id, [Bind("Id,Image")] User ChangePhotoUser, IFormFile file)
        {
            if (id != ChangePhotoUser.Id)
            {
                _logger.LogError("Curso não encontrado");
                return NotFound();
            }

            var user = await _userRepository.GetById(id);

            if (ModelState.IsValid)
            {
                if (file != null)
                {
                    _logger.LogInformation("Criando link da pasta");
                    var linkUpload = Path.Combine(_hostingEnviroment.WebRootPath, "imagesUsers");
                    string nameImg = System.Guid.NewGuid().ToString() + ".jpg";
                    string backupNameImg = user.Photo;
                    

                    using (FileStream fileStream = new FileStream(Path.Combine(linkUpload, nameImg), FileMode.Create))
                    {
                        _logger.LogInformation("Copiando arquivo para a pasta");
                        await file.CopyToAsync(fileStream);
                        _logger.LogInformation("Arquivo copiado");
                        user.Photo = "~/imagesUsers/" + nameImg;

                        _logger.LogInformation("Atualizando usuário");
                        await _userRepository.UpdateUser(user);
                        if (backupNameImg != null)
                        {
                            System.IO.File.Delete(Path.Combine(linkUpload, backupNameImg));
                        }
                        return RedirectToAction(nameof(Index));
                    }
                }
            }
            return View(user);
        }

        [HttpGet]
        public async Task<IActionResult> RedefinePassword(string UserId)
        {
            _logger.LogInformation("Verificando se o usuário existe");
            var user = await _userRepository.GetById(UserId);

            var redefinePasswordViewModel = new RedefinePasswordViewModel
            {
                UserId = user.Id,
                UpdateDate = DateTime.Now,
                Name = user.Name,
            };

            _logger.LogInformation("Atualizar usuário");
            return View(redefinePasswordViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RedefinePassword(RedefinePasswordViewModel redefinePasswordViewModel)
        {

            if (ModelState.IsValid)
            {
                _logger.LogInformation("Pegando usuário pela matrícula");
                var user = await _userRepository.GetById(redefinePasswordViewModel.UserId);
                PasswordHasher<User> passwordHasher = new PasswordHasher<User>();
                DateTime date = redefinePasswordViewModel.UpdateDate;

                if (passwordHasher.VerifyHashedPassword(user, user.PasswordHash, redefinePasswordViewModel.Password) != PasswordVerificationResult.Failed)
                {
                    _logger.LogInformation("Informações corretas. Atualizando usuário");

                    user.UpdateDate = date;
                    user.PasswordHash = passwordHasher.HashPassword(user, redefinePasswordViewModel.NewPassword);

                    await _userRepository.UpdateUser(user);

                    return RedirectToAction("Index", "Users");
                }

                _logger.LogInformation("Informações inválidas");
                ModelState.AddModelError("", "Senha inválida");

            }
            _logger.LogError("Informações inválidas");

            return View(redefinePasswordViewModel);
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
                var user = await _userRepository.GetUserByMatricula(login.Matricula);
                PasswordHasher<User> passwordHasher = new PasswordHasher<User>();

                if (user != null)
                {
                    _logger.LogInformation("Verificando informações do usuário");
                    if (passwordHasher.VerifyHashedPassword(user, user.PasswordHash, login.Password) != PasswordVerificationResult.Failed)
                    {
                        _logger.LogInformation("Informações corretas. Logando usurário");
                        await _userRepository.Login(user, false);

                        return RedirectToAction("Index", "Home");
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
