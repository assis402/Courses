﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Courses.ViewModels
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Campo Obrigatório")]
        [StringLength(100, ErrorMessage = "Use menos de 100 caracteres")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Campo Obrigatório")]
        public string CPF { get; set; }

        [Required(ErrorMessage = "Campo Obrigatório")]
        public string Telefone { get; set; }

        [Required(ErrorMessage = "Campo Obrigatório")]
        [StringLength(100, ErrorMessage = "Use menos de 100 caracteres")]
        public string NomeUser { get; set; }

        [Required(ErrorMessage = "Campo Obrigatório")]
        [DataType(DataType.EmailAddress, ErrorMessage = "E-mail inválido")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Campo Obrigatório")]
        [StringLength(100, ErrorMessage = "Use menos de 100 caracteres")]
        [DataType(DataType.Password)]
        public string Senha { get; set; }

    }
}