using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace Courses.Models
{
    public class AccessLevels : IdentityRole
    {
        public string Description { get; set; }
    }
}
