using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Courses.Models
{
    public class User : IdentityUser
    {
        //public string UserId { get; set; }

        /*public Guid GetId()
        {
            return id;
        }

        public void SetId(Guid value)
        {
            id = value;
        }*/
        public string Nome { get; set; }

        public string CPF { get; set; }

        public string Telefone { get; set; }

        public string Email { get; set; }
        public ICollection<UserCourse> UserCourses { get; set; }
        public string Matricula { get; set; }
        public DateTime DataCriacao { get; set; }
        public DateTime DataAtualizacao { get; set; }
        public Wallet Wallet { get; set; }

    }
}
