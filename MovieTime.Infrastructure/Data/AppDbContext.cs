using Microsoft.EntityFrameworkCore;
using MovieTime.Core.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace MovieTime.Infrastructure.Data
{
    public class AppDbContext : DbContext
    {

        public AppDbContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<User> Users { get; set; }
        public DbSet<Movie> Movies {get; set; }

        public DbSet<Favourite> Favourities {get; set;}

        protected override void OnModelCreating(ModelBuilder builder)
        {
                base.OnModelCreating(builder);
                builder.Entity<User>();
                builder.Entity<Movie>();

                builder.Entity<Favourite>();


            /*                builder.Entity<Favourities>().HasMany<User>(g => (IEnumerable<User>)g.Users);
            */
            /*            this.HasRequired(c => c.Owner).WithMany(p => p.Cars).HasForeignKey(c => c.OwnerId);
            */
        }
        /* modelBuilder.Entity<Grade>()
     .HasMany<Student>(g => g.Students)
     .WithRequired(s => s.CurrentGrade)
     .HasForeignKey<int>(s => s.CurrentGradeId);*/
    }
}
