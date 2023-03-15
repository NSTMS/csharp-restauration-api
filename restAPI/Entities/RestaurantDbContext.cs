using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace restAPI.Entities
{
    public class RestaurantDbContext : DbContext
    {
        private string _connectionString =
            "Server=DESKTOP-387U7IH;;Database=RestaurantDb;User Id=sa;Password=!Yosi123123";
        public DbSet<Restaurant> Restaurants { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Dish> Dishes { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Restaurant>()
                .Property(r => r.Name)
                .IsRequired()
                .HasMaxLength(25);

            modelBuilder.Entity<User>()
               .Property(r => r.Email)
               .IsRequired();

            modelBuilder.Entity<Role>()
             .Property(r => r.Name)
             .IsRequired();

            modelBuilder.Entity<Dish>()
                .Property(r => r.Name)
                .IsRequired();

            /*modelBuilder.Entity<Restaurant>().HasData();*/

            modelBuilder.Entity<Address>()
                .Property(a => a.City)
                .IsRequired()
                .HasMaxLength(50);

            modelBuilder.Entity<Address>()
             .Property(a => a.Street)
             .IsRequired()
             .HasMaxLength(50);

            }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_connectionString);
        }
    } 
}
