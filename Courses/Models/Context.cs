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
        public DbSet<Matriculation> Matriculations { get; set; }
        public DbSet<Upgrade> Upgrades { get; set; }
        public DbSet<Feature> Features { get; set; }


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
            builder.ApplyConfiguration(new MatriculationMap());
            builder.ApplyConfiguration(new UpgradeMap());
            builder.ApplyConfiguration(new FeatureMap());
        }


    }
}
