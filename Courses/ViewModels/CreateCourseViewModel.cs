using System.ComponentModel.DataAnnotations;

namespace Courses.ViewModels
{
    public class CreateCourseViewModel
    {
        public string UserId { get; set; }

        [Required(ErrorMessage = "Campo Obrigatório")]
        [StringLength(100, ErrorMessage = "Use menos de 100 caracteres")]
        public string Title { get; set; }

        public string Image { get; set; }

        [Required(ErrorMessage = "Campo Obrigatório")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Campo Obrigatório")]
        public int CourseLoad { get; set; }

        [Required(ErrorMessage = "Campo Obrigatório")]
        public string Price { get; set; }
    }
}
