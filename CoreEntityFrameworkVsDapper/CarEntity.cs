using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoreEntityFrameworkVsDapper.Entity
{
    public class BloggingContext : DbContext
    {
        public DbSet<Cars> Cars { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(@"User ID=postgres;Password=yandrakar666;Host=odin;Port=5432;Database=Vehicles;Pooling=true;MinPoolSize=5;MaxPoolSize=20;");
        }
    }

    public class Cars
    {
        public int Id { get; set; }

        public String Model { get; set; }

        public String Registration { get; set; }

        public String Color { get; set; }
    }
}
