using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Courses.Models;
using Courses.AccessData.Interfaces;
using Microsoft.Extensions.Logging;
using System.Text;
using Courses.ViewModels;

namespace Courses.Controllers
{
    public class MatriculationController : Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly IWalletRepository _walletRepository;
        private readonly IMatriculationRepository _matriculationRepository;
        private readonly ICourseRepository _courseRepository;
        private readonly ILogger<MatriculationController> _logger;

        public MatriculationController(IUserRepository userRepository, IWalletRepository walletRepository, IMatriculationRepository matriculationRepository, ICourseRepository courseRepository, ILogger<MatriculationController> logger)
        {
            _userRepository = userRepository;
            _walletRepository = walletRepository;
            _matriculationRepository = matriculationRepository;
            _courseRepository = courseRepository;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> Matriculate(string courseId)
        {
            _logger.LogInformation("Nova atualização");
            User user = await _userRepository.GetLoggedUser(User);
            Course course = await _courseRepository.GetById(courseId);

            var matriculateViewModel = new MatriculateViewModel
            {
                UserId = user.Id,
                UserWalletBalance = _walletRepository.GetBalanceByUserId(user.Id),
                CourseId = courseId,
                CourseName = course.Nome,
                Price = course.Preco
            };

            return View(matriculateViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Matriculate(MatriculateViewModel matriculateViewModel)
        {
            if (ModelState.IsValid)
            {
                if (await _matriculationRepository.AlreadyMatriculate(matriculateViewModel.UserId, matriculateViewModel.CourseId))
                {
                    _logger.LogInformation("Usuário já possui matrícula nesse curso");
                    return RedirectToAction("Index", "Courses", "");
                }
                else if (matriculateViewModel.Price > matriculateViewModel.UserWalletBalance)
                {
                    _logger.LogInformation("Saldo insuficiente");
                    TempData["ErrorMessage"] = "Saldo insuficiente";
                    return View(matriculateViewModel);
                }
                else
                {
                    DateTime purchaseDate = DateTime.Now;
                    //string generatedIdentifier = GenerateIdentifier(10);

                    Matriculation matriculation = new Matriculation
                    {
                        UserId = matriculateViewModel.UserId,
                        CourseId = matriculateViewModel.CourseId,
                        TotalValue = matriculateViewModel.Price,
                        PurchaseDate = purchaseDate,
                        ExternalIdentifier = null
                    };

                    //_logger.LogInformation("Enviando email com detalhes da matrícula");
                    //string subject = "Matrícula concluída com sucesso";

                    // string message = string.Format("Seu veículo já o aguarda. Você poderá pegá-lo dia {0}" +
                    //   " e deverá devolvê-lo dia {1}. O preço será R${2},00. Divirtá-se !!! ", aluguel.Inicio, aluguel.Fim, aluguel.PrecoTotal);

                    //  await _email.EnviarEmail(usuario.Email, assunto, mensagem);

                    await _matriculationRepository.Insert(matriculation);
                    _logger.LogInformation("Matrícula feita");

                    _logger.LogInformation("Atualizando saldo do usuario");
                    var userWallet = await _walletRepository.GetWalletByUserId(matriculateViewModel.UserId);
                    userWallet.Balance -= matriculation.TotalValue;
                    await _walletRepository.Update(userWallet);
                    _logger.LogInformation("Saldo atualizado");

                    return RedirectToAction("Index", "Courses");
                }
            }
            _logger.LogInformation("Informações inválidas");
            return View();
        }

        public string GenerateIdentifier(int size)
        {
            var builder = new StringBuilder(size);
            Random _random = new Random();

            const int lettersOffset = 26;

            for (var i = 0; i < size; i++)
            {
                var @char = (char)_random.Next(lettersOffset);
                builder.Append(@char);
            }

            return builder.ToString();
        }
    }
}
