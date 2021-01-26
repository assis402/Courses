using Courses.Models;
using System.ComponentModel.DataAnnotations;

namespace Courses.ViewModels
{
    public class RegisterViewModel
    {

        [Required(ErrorMessage = "Campo Obrigatório")]
        [StringLength(100, ErrorMessage = "Use menos de 100 caracteres")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Campo Obrigatório")]
        [StringLength(10, ErrorMessage = "Use menos de 10 caracteres")]
        public string Nickname { get; set; }

        [Required(ErrorMessage = "Campo Obrigatório")]
        [CustomValidationCPF(ErrorMessage = "CPF inválido")]
        public string CPF { get; set; }

        [Required(ErrorMessage = "Campo Obrigatório")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Campo Obrigatório")]
        [DataType(DataType.EmailAddress, ErrorMessage = "E-mail inválido")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Campo Obrigatório")]
        [DataType(DataType.EmailAddress, ErrorMessage = "E-mail inválido")]
        [Compare("Email", ErrorMessage = "Essas senhas não são iguais")]
        public string ConfirmEmail { get; set; }

    }
}
