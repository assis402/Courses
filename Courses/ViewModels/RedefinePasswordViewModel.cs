﻿using System;
using System.ComponentModel.DataAnnotations;

namespace Courses.ViewModels
{
    public class RedefinePasswordViewModel
    {
        public string Id { get; set; }

        public DateTime DataAtualizacao { get; set; }

        public string Nome { get; set; }

        [Required(ErrorMessage = "Campo Obrigatório")]
        [StringLength(100, ErrorMessage = "Use menos de 100 caracteres")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Campo Obrigatório")]
        [StringLength(100, ErrorMessage = "Use menos de 100 caracteres")]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }

        [Required(ErrorMessage = "Campo Obrigatório")]
        [StringLength(100, ErrorMessage = "Use menos de 100 caracteres")]
        [Compare("NewPassword", ErrorMessage = "Essas senhas não são iguais")]
        public string ConfirmNewPassword { get; set; }
    }
}
