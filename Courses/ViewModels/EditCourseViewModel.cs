using System;
using System.ComponentModel.DataAnnotations;

namespace Courses.ViewModels
{
    public class EditCourseViewModel

    {
        public string CourseId { get; set; }

        public DateTime DataAtualizacao { get; set; }

        [Required(ErrorMessage = "Campo Obrigatório")]
        [StringLength(100, ErrorMessage = "Use menos de 100 caracteres")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Campo Obrigatório")]
        public string Foto { get; set; }

        [Required(ErrorMessage = "Campo Obrigatório")]
        public int CargaHoraria { get; set; }

        [Required(ErrorMessage = "Campo Obrigatório")]
        public string Preco { get; set; }
    }
}
