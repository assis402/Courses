using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

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
