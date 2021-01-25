using System.ComponentModel.DataAnnotations;

namespace Courses.ViewModels
{
    public class CreateCourseViewModel
    {
        public string UserId { get; set; }

        [Required(ErrorMessage = "Campo Obrigatório")]
        [StringLength(100, ErrorMessage = "Use menos de 100 caracteres")]
        public string Nome { get; set; }

        public string Foto { get; set; }

        [Required(ErrorMessage = "Campo Obrigatório")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Campo Obrigatório")]
        public int CargaHoraria { get; set; }

        [Required(ErrorMessage = "Campo Obrigatório")]
        public string Preco { get; set; }
    }
}
