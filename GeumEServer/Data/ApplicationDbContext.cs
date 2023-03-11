using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeumEServer.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Dog> Dogs { get; set; }
        public DbSet<Walk> Walks { get; set; }
        public DbSet<Place> Places { get; set; }
        public DbSet<Msg> Msgs { get; set; }

        public DbSet<WalkPlace> WalkPlaces { get; set; }

        public DbSet<WalkDog> WalkDogs { get; set; }
        public DbSet<UserDog> UserDogs { get; set; }
        public DbSet<Walking> Walkings { get; set; }
        public DbSet<Ranking> Rankings { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            :base(options)
        {

        }
    }
}
