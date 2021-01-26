using System;
using System.Collections.Generic;

namespace Courses.Models
{
    public class Upgrade
    {
        public string UpgradeId { get; set; }
        public string Title { get; set; }
        public DateTime CreationDate { get; set; }
        public ICollection<Feature> Features { get; set; }
        public User User { get; set; }
        public string UserId { get; set; }
    }
}
