using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Courses.Models;
using System.Dynamic;
using Courses.AccessData.Interfaces;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using System;
using Courses.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace Courses.Controllers
{
    public class HomeController : Controller
    {
        private readonly IUpgradeRepository _upgradeRepository;
        private readonly IFeatureRepository _featureRepository;
        private readonly IUserRepository _userRepository;
        private readonly ILogger<CoursesController> _logger;

        public HomeController(IUpgradeRepository upgradeRepository, IFeatureRepository featureRepository, IUserRepository userRepository, ILogger<CoursesController> logger)
        {
            _upgradeRepository = upgradeRepository;
            _featureRepository = featureRepository;
            _userRepository = userRepository;
            _logger = logger;
        }

        public async Task<IActionResult> Index()
         {
            dynamic mymodel = new ExpandoObject();
            mymodel.Upgrades = await _upgradeRepository.GetLastFive().ToListAsync();
            mymodel.Features = await _featureRepository.GetLastFiveByStatus(FeatureStatus.Proposta).ToListAsync();
            return View(mymodel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> InsertFeature([Bind("Title,Complexibility")] Feature feature)
        {
            User user = await _userRepository.GetLoggedUser(User);

            if (ModelState.IsValid)
            {
                _logger.LogInformation("Inserindo feature");
                feature.UserId = user.Id;
                feature.Date = DateTime.Now;

                await _featureRepository.Insert(feature);
                return RedirectToAction(nameof(Index));
            }
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> InsertUpgrade()
        {
            _logger.LogInformation("Nova atualização");
            List<Feature> featuresPropostas = await _featureRepository.GetAllByStatus(FeatureStatus.Proposta).ToListAsync();
            ViewData["FeaturesId"] = new SelectList(featuresPropostas, "FeatureId", "Title");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> InsertUpgrade(InsertUpgradeViewModel insertUpgradeViewModel)
        {
            User user = await _userRepository.GetLoggedUser(User);

            if (ModelState.IsValid)
            {
                _logger.LogInformation("Inserindo atualização");

                Upgrade upgrade = new Upgrade()
                {
                    Date = DateTime.Now,
                    Title = insertUpgradeViewModel.Title,
                    UserId = user.Id
                };

                await _upgradeRepository.Insert(upgrade);

                _logger.LogInformation("Atribuindo esta atualização às features");

                foreach (String FeatureId in insertUpgradeViewModel.FeaturesId)
                {
                    Feature feature = await _featureRepository.GetById(FeatureId);
                    feature.UpgradeId = upgrade.UpgradeId;
                    feature.Status = FeatureStatus.Desenvolvida;

                    await _featureRepository.Update(feature);
                }

                return RedirectToAction(nameof(Index));
            }
            return View();
        }
    }
}
