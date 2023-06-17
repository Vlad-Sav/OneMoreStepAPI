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
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasMany(u => u.UsersStickers)
                .WithOne(us => us.User)
                .HasForeignKey(us => us.UserId);
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Route> Routes { get; set; }
        public DbSet<Sticker> Stickers { get; set; }
        public DbSet<RoutesPicture> RoutesPictures { get; set; }
        public DbSet<UsersStepsCount> UsersStepsCount { get; set; }
        public DbSet<UsersPinnedSticker> UsersPinnedStickers { get; set; }
        public DbSet<RoutesLikes> RoutesLikes { get; set; }
        public DbSet<UsersStickers> UsersStickers { get; set; }
        public DbSet<Level> Levels { get; set; }
        public DbSet<Progress> Progress { get; set; }
        public DbSet<Chest> Chests { get; set; }
        public DbSet<ProfilePhotos> ProfilePhotos { get; set; }
    }
}
