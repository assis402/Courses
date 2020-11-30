using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace Courses.Models
{
    public class AccessLevels : IdentityRole
    {
        [Required(ErrorMessage = "Campo Obrigatório")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Campo Obrigatório")]
        public string Description { get; set; }

        public AccessLevels()
        {
        }
    }
}
