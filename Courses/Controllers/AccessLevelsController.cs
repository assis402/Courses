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
    public class AccessLevelsController : Controller
    {
        private readonly IAccessLevelsRepository _accessLevelsRepository;
        private readonly ILogger<AccessLevelsController> _logger;

        public AccessLevelsController(IAccessLevelsRepository accessLevelsRepository, ILogger<AccessLevelsController> logger)
        {
            _accessLevelsRepository = accessLevelsRepository;
            _logger = logger;
        }

        // GET: AccessLevels
        public async Task<IActionResult> Index()
        {
            _logger.LogInformation("Listando todos os registros");
            return View(await _accessLevelsRepository.PickAll().ToListAsync());
        }

        // GET: AccessLevels/Create
        public IActionResult Create()
        {
            _logger.LogInformation("Iniciando criação de níveis de acesso");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Description,Name")] AccessLevels accessLevels)
        {
            if (ModelState.IsValid)
            {
                _logger.LogInformation("Verificando se o nível de acesso existe");
                bool accessLevelExist = await _accessLevelsRepository.AccessLevelExists(accessLevels.Name);

                if (!accessLevelExist)
                {
                    accessLevels.NormalizedName = accessLevels.Name.ToUpper();
                    await _accessLevelsRepository.Insert(accessLevels);
                    _logger.LogInformation("Novo nível de acesso criado");

                    return RedirectToAction("Index", "AccessLevels");
                }
            }

            _logger.LogError("Informações inválidas");
            return View(accessLevels);
        }

        // GET: AccessLevels/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            _logger.LogInformation("Atualizando nível de acesso");
            if (id == null)
            {
                _logger.LogInformation("Nível não encontrado");
                return NotFound();
            }

            var accessLevels = await _accessLevelsRepository.PickById(id);
            if (accessLevels == null)
            {
                return NotFound();
            }
            return View(accessLevels);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Description,Id,Name,NormalizedName,ConcurrencyStamp")] AccessLevels accessLevels)
        {
            if (id != accessLevels.Id)
            {
                _logger.LogInformation("Nível não encontrado");
                return NotFound();
            }

            if (ModelState.IsValid)
            {

                await _accessLevelsRepository.Update(accessLevels);
                _logger.LogInformation("Nível atualizado");
                return RedirectToAction("Index","AccessLevels");

            }
            _logger.LogError("Informações inválidas");
            return View(accessLevels);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(string id)
        {
            await _accessLevelsRepository.Delete(id);
            _logger.LogInformation("Nível deletado");
            return RedirectToAction("Index", "AccessLevels");
        }
    }
}
