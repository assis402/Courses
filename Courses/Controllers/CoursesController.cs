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
                        createCourseViewModel.Image = "~/imagesCourses/" + nameImg;
                    }

                    _logger.LogInformation("Salvando novo curso");

                    createCourseViewModel.Price = RemoveNonNumeric(createCourseViewModel.Price);

                    Course course = new Course()
                    {
                        Title = createCourseViewModel.Title,
                        Image = createCourseViewModel.Image,
                        Description = createCourseViewModel.Description,
                        CourseLoad = createCourseViewModel.CourseLoad,
                        CreationDate = DateTime.Now,
                        UpdateDate = DateTime.Now,
                        Price = double.Parse(createCourseViewModel.Price)/100,
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

            TempData["imagesCourses"] = course.Image;

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
                Description = course.Description,
                Title = course.Title,
                //Image = course.Image,
                Price = course.Price.ToString(),
                CourseLoad = course.CourseLoad
            };

            return View(editCourseViewModel);
        }


        // POST: Course/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([Bind("CourseId,Title,Description,Image,CourseLoad,Price")] EditCourseViewModel editCourseViewModel)
        {
            if (ModelState.IsValid)
            {
                /*if (file != null)
                {
                    _logger.LogInformation("Criando link da pasta");
                    var linkUpload = Path.Combine(_hostingEnviroment.WebRootPath, "imagesCourses");
                    string nameImg = System.Guid.NewGuid().ToString() + ".jpg";

                    using (FileStream fileStream = new FileStream(Path.Combine(linkUpload, nameImg), FileMode.Create))
                    {
                        _logger.LogInformation("Copiando arquivo para a pasta");
                        await file.CopyToAsync(fileStream);
                        _logger.LogInformation("Arquivo copiado");
                        course.Image = "~/imagesCourses/" + nameImg;
                    }
                }

                else course.Image = TempData["imagesCourses"].ToString();*/

                Course course = await _courseRepository.GetById(editCourseViewModel.CourseId);

                editCourseViewModel.Price = RemoveNonNumeric(editCourseViewModel.Price);

                course.Title = editCourseViewModel.Title;
                //course.Image = editCourseViewModel.Image;
                course.Description = editCourseViewModel.Description;
                course.CourseLoad = editCourseViewModel.CourseLoad;
                course.UpdateDate = DateTime.Now;
                course.Price = double.Parse(editCourseViewModel.Price) / 100;
                //course.UserId = editCourseViewModel.UserId;
  

                _logger.LogInformation("Atualizando curso");
                await _courseRepository.Update(course);
                return RedirectToAction(nameof(Index));
            }
            return View(editCourseViewModel);
        }

        // POST: Course/Delete/5
        [HttpPost]
        public async Task<JsonResult> Delete(string id)
        {
            var course = await _courseRepository.GetById(id);

            string imageCourse = course.Image;

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
