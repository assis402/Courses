using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Courses.Models
{
    public class UserCourse
    {
        public string UserCourseId { get; set; }
        public string UserId { get; set; }
        public User User { get; set; }
        public string CourseId { get; set; }
        public Course Course { get; set; }
        public DateTime PurchaseDate { get; set; }
        public int Progress { get; set; }
    }

}
