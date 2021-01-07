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

            protected override void OnModelCreating(ModelBuilder builder)
            {
                base.OnModelCreating(builder);
                builder.Entity<User>();
            }
        
    }
}
