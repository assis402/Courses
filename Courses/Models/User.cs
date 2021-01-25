using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace Courses.Models
{
    public class User : IdentityUser
    {
        public string Nome { get; set; }
        public string CPF { get; set; }
        public string Telefone { get; set; }
        public ICollection<Matriculation> UserCourses { get; set; }
        public ICollection<Feature> Features { get; set; }
        public ICollection<Upgrade> Upgrades { get; set; }
        public string Matricula { get; set;  }
        public DateTime DataCriacao { get; set; }
        public DateTime DataAtualizacao { get; set; }
        public string Foto { get; set; }
        public Wallet Wallet { get; set; }

        public User()
        {
        }

        

    }

    
}
