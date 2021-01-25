using System.ComponentModel.DataAnnotations;

namespace Courses.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Campo obrogatório")]
        public string Matricula { get; set; }
        [DataType(DataType.Password, ErrorMessage = "Senha inválida")]
        public string Password { get; set; }


    }
}
