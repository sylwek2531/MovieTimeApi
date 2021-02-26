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
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Favourite> Favourities { get; set; }
        public DbSet<Creator> Creators { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Rated> Rateds { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<User>().HasMany(e => e.Movies).WithOne(e=> e.Users).HasForeignKey(e=> e.ID).OnDelete(DeleteBehavior.NoAction);
            builder.Entity<Movie>().HasOne(e => e.Users).WithMany(e => e.Movies).HasForeignKey(e => e.UserID).OnDelete(DeleteBehavior.NoAction);
            builder.Entity<Favourite>();
            builder.Entity<Creator>();
            builder.Entity<Genre>();
            builder.Entity<Rated>();

        }
    
    }
}
