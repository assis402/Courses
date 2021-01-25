using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Courses.Models;
using Courses.AccessData.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;
using System.IO;
using Courses.ViewModels;

namespace Courses.Controllers
{
    public class CoursesController : Controller
    {
        private readonly ICourseRepository _courseRepository;
        private readonly IUserRepository _userRepository;
        private readonly IHostingEnvironment _hostingEnviroment;
        private readonly ILogger<CoursesController> _logger;

        public CoursesController(ICourseRepository courseRepository, IUserRepository userRepository, IHostingEnvironment hostingEnviroment, ILogger<CoursesController> logger)
        {
            _courseRepository = courseRepository;
            _userRepository = userRepository;
            _hostingEnviroment = hostingEnviroment;
            _logger = logger;
        }



        // GET: Course
        public async Task<IActionResult> Index()
        {
            _logger.LogInformation("Listando todos os cursos");
            return View(await _courseRepository.GetAll().ToListAsync());
        }

        // GET: Course/Create
        public IActionResult Create()
        {
            _logger.LogInformation("Novo curso");
            return View();
        }

        // POST: Course/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateCourseViewModel createCourseViewModel, IFormFile file)
        {
            if (ModelState.IsValid)
            {
                if (file != null)
                {
                    _logger.LogInformation("Criando link da pasta");
                    var linkUpload = Path.Combine(_hostingEnviroment.WebRootPath, "imagesCourses");
                    string nameImg = System.Guid.NewGuid().ToString() + ".jpg";

                    using (FileStream fileStream = new FileStream(Path.Combine(linkUpload, nameImg), FileMode.Create))
                    {

                        _logger.LogInformation("Copiando arquivo para a pasta");
                        await file.CopyToAsync(fileStream);
                        _logger.LogInformation("Arquivo copiado");
                        createCourseViewModel.Foto = "~/imagesCourses/" + nameImg;
                    }

                    _logger.LogInformation("Salvando novo curso");

                    createCourseViewModel.Preco = RemoveNonNumeric(createCourseViewModel.Preco);

                    var course = new Course
                    {
                        Nome = createCourseViewModel.Nome,
                        Foto = createCourseViewModel.Foto,
                        Description = createCourseViewModel.Description,
                        CargaHoraria = createCourseViewModel.CargaHoraria,
                        DataCriacao = DateTime.Now,
                        DataAtualizacao = DateTime.Now,
                        Preco = double.Parse(createCourseViewModel.Preco)/100,
                        UserId = createCourseViewModel.UserId
                    };

                    await _courseRepository.Insert(course);
                    return RedirectToAction(nameof(Index));
                }

            }
            _logger.LogError("Informações inválidas");
            return View(createCourseViewModel);
        }

        public async Task<IActionResult> Details(string id)
        {
            _logger.LogInformation("Visualizando detalhes");

            var course = await _courseRepository.GetById(id);

            if (course == null)
            {
                _logger.LogError("Curso não encontrado");
                return NotFound();
            }

            TempData["imagesCourses"] = course.Foto;

            return View(course);
        }

        // GET: Course/Edit/5
        public async Task<IActionResult> Edit(string CourseId)
        {

            _logger.LogInformation("Atualizar curso");

            var course = await _courseRepository.GetById(CourseId);
            if (course == null)
            {
                _logger.LogError("Curso não encontrado");
                return NotFound();
            }

            var editCourseViewModel = new EditCourseViewModel
            {
                CourseId = course.CourseId,
                DataAtualizacao = DateTime.Now,
                Nome = course.Nome,
                Foto = course.Foto,
                Preco = course.Preco.ToString(),
                CargaHoraria = course.CargaHoraria
            };

            return View(editCourseViewModel);
        }


        // POST: Course/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("CourseId,Nome,Foto,CargaHoraria,DataCriacao,DataAtualizacao,Preco")] Course course, IFormFile file)
        {
            if (id != course.CourseId)
            {
                _logger.LogError("Curso não encontrado");
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                if (file != null)
                {
                    _logger.LogInformation("Criando link da pasta");
                    var linkUpload = Path.Combine(_hostingEnviroment.WebRootPath, "imagesCourses");
                    string nameImg = System.Guid.NewGuid().ToString() + ".jpg";

                    using (FileStream fileStream = new FileStream(Path.Combine(linkUpload, nameImg), FileMode.Create))
                    {
                        _logger.LogInformation("Copiando arquivo para a pasta");
                        await file.CopyToAsync(fileStream);
                        _logger.LogInformation("Arquivo copiado");
                        course.Foto = "~/imagesCourses/" + nameImg;
                    }
                }

                else course.Foto = TempData["imagesCourses"].ToString();

                _logger.LogInformation("Atualizando curso");
                await _courseRepository.Update(course);
                return RedirectToAction(nameof(Index));
            }
            return View(course);
        }

        // POST: Course/Delete/5
        [HttpPost]
        public async Task<JsonResult> DeleteConfirmed(string id)
        {
            var course = await _courseRepository.GetById(id);

            string imageCourse = course.Foto;

            imageCourse = imageCourse.Replace("~", "wwwroot");
            System.IO.File.Delete(imageCourse);

            _logger.LogInformation("Excluindo curso");
            await _courseRepository.Delete(id);
            return Json("Curso excluído");
        }

        public static string RemoveNonNumeric(string text)
        {
            System.Text.RegularExpressions.Regex reg = new System.Text.RegularExpressions.Regex(@"[^0-9,.]");
            string ret = reg.Replace(text, string.Empty);
            return ret;
        }

    }
}
