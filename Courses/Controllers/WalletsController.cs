using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Courses.Models;
using Courses.AccessData.Interfaces;
using Microsoft.Extensions.Logging;

namespace Courses.Controllers
{
    public class WalletsController : Controller
    {
        private readonly IWalletRepository _walletRepository;
        private readonly IUserRepository _userRepository;
        private readonly ILogger<WalletsController> _logger;

        public WalletsController(IWalletRepository walletRepository, IUserRepository userRepository, ILogger<WalletsController> logger)
        {
            _walletRepository = walletRepository;
            _userRepository = userRepository;
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            _logger.LogInformation("Listando os saldos");
            return View(await _walletRepository.GetAll());
        }

        public IActionResult Create()
        {
            _logger.LogInformation("Criar novo saldo");
            ViewData["UserId"] = new SelectList(_userRepository.GetAll(), "Id", "Email");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("WalletId,UserId,Balance")] Wallet wallet)
        {
            if (ModelState.IsValid)
            {
                await _walletRepository.Insert(wallet);
                _logger.LogInformation("Novo saldo criado");
                return RedirectToAction(nameof(Index));
            }
            _logger.LogError("Informações inválidas");
            ViewData["UserId"] = new SelectList(_userRepository.GetAll(), "Id", "Email", wallet.UserId);
            return View(wallet);
        }

        public async Task<IActionResult> Edit(string id)
        {
            _logger.LogInformation("Atualizar conta");

            var wallet = await _walletRepository.GetById(id);
            if (wallet == null)
            {
                _logger.LogError("Conta não encontrada");
                return NotFound();
            }
            _logger.LogError("Informações inválidas");
            //ViewData["UserId"] = new SelectList(_userRepository.PickAll(), "Id", "Email", wallet.UserId);
            return View(wallet);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("WalletId,UserId,Saldo")] Wallet wallet)
        {
            if (id != wallet.WalletId)
            {
                _logger.LogError("Conta não encontrada");
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await _walletRepository.Update(wallet);
                _logger.LogInformation("Atualizando conta");
                return RedirectToAction(nameof(Index));
            }
            _logger.LogError("Informações Inválidas");
            ViewData["UserId"] = new SelectList(await _walletRepository.GetAll(), "Id", "Email", wallet.UserId);
            return View(wallet);
        }
    }
}
