using System;
using System.Collections.Generic;

namespace Courses.Models
{
    public class Course
    {
        public string CourseId { get; set; }
        public string Title { get; set; }
        public string Image { get; set; }
        public string Description { get; set; }
        public int CourseLoad { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime UpdateDate { get; set; }
        public double Price { get; set; }
        public User User { get; set; }
        public string UserId { get; set; }
        public ICollection<Matriculation> Matriculations { get; set; }

        public Course()
        {
        }

        public Course(string title, string image, string description, int courseLoad, DateTime creationDate, DateTime updateDate, double price, string userId)
        {
            Title = title;
            Image = image;
            Description = description;
            CourseLoad = courseLoad;
            CreationDate = creationDate;
            UpdateDate = updateDate;
            Price = price;
            UserId = userId;
        }
    }


}
