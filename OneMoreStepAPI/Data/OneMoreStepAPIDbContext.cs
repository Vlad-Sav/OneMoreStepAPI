using Microsoft.EntityFrameworkCore;
using OneMoreStepAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OneMoreStepAPI.Data
{
    public class OneMoreStepAPIDbContext : DbContext
    {
        public OneMoreStepAPIDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Route> Routes { get; set; }
        public DbSet<RoutesPicture> RoutesPictures { get; set; }
        public DbSet<UsersStepsCount> UsersStepsCount { get; set; }
    }
}
