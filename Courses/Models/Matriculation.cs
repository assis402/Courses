using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Courses.Models
{
    public class Matriculation
    {
        public string MatriculationId { get; set; }
        public string UserId { get; set; }
        public User User { get; set; }
        public string CourseId { get; set; }
        public Course Course { get; set; }
        public DateTime PurchaseDate { get; set; }
        public double TotalValue { get; set; }
        public string ExternalIdentifier { get; set; }
    }

}
