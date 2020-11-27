using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Courses.Mapping;

namespace Courses.Models
{
    public class Context : IdentityDbContext<User, AccessLevels, string>
    {
        public DbSet<User> Users { get; set; }
        public DbSet<AccessLevels> AccessLevels { get; set; }
        public DbSet<Wallet> Wallets { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<UserCourse> UserCourses { get; set; }


        public Context(DbContextOptions<Context> options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfiguration(new UserMap());
            builder.ApplyConfiguration(new AccessLevelsMap());
            builder.ApplyConfiguration(new WalletMap());
            builder.ApplyConfiguration(new CourseMap());
            builder.ApplyConfiguration(new UserCourseMap());

        }


    }
}
