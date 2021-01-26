using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace Courses.Models
{
    public class User : IdentityUser
    {
        public string UserId { get; set; }
        public string Name { get; set; }
        public string CPF { get; set; }
        public ICollection<Matriculation> Matriculations { get; set; }
        public ICollection<Feature> Features { get; set; }
        public ICollection<Upgrade> Upgrades { get; set; }
        public ICollection<Course> Courses { get; set; }
        public string Matricula { get; set;  }
        public DateTime CreationDate { get; set; }
        public DateTime UpdateDate { get; set; }
        public string Photo { get; set; }
        public Wallet Wallet { get; set; }

        public User()
        {
        }

        public User(string userId, string userName, string normalizedUserName, string passwordHash, string securityStamp, string name, string cPF, string phoneNumber, string email, string matricula, DateTime creationDate, DateTime updateDate, string photo)
        {
            UserId = userId;
            UserName = userName;
            NormalizedUserName = normalizedUserName;
            PasswordHash = passwordHash;
            SecurityStamp = securityStamp;
            Name = name;
            CPF = cPF;
            PhoneNumber = phoneNumber;
            Email = email;
            Matricula = matricula;
            CreationDate = creationDate;
            UpdateDate = updateDate;
            Photo = photo;
        }
    }

    
}
