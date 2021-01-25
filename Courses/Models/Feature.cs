using System;

namespace Courses.Models
{
    public class Feature
    {
        public string FeatureId { get; set; }
        public string Title { get; set; }
        public DateTime Date { get; set; }
        public FeatureComplexibility Complexibility { get; set; }
        public FeatureStatus Status { get; set; }
        public Upgrade Upgrade { get; set; }
        public string UpgradeId { get; set; }
        public User User { get; set; }
        public string UserId { get; set; }
    }
}
