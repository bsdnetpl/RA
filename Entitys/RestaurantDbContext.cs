﻿using Microsoft.EntityFrameworkCore;
using RA.Entitys;
namespace RA.Entitys
{
    public class RestaurantDbContext : DbContext
    {
        private string _connectionString = "Server=(localdb)\\mssqllocaldb;Database=RestaurantDB;Trusted_Connection=True;";
        public DbSet<Restaurant> Restaurants {set; get;}
        public DbSet<Address> Addresses { set; get; }
        public DbSet<Dish> Dishes { set; get; }
        public DbSet<User> Users { set; get; }
        public DbSet<Role> Roles { set; get; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .Property(r => r.Email)
                .IsRequired();

            modelBuilder.Entity<Role>()
            .Property(r => r.Name)
            .IsRequired();

            modelBuilder.Entity<Restaurant>()
                  .Property(e => e.Name)
                  .IsRequired()
                  .HasMaxLength(25);

            modelBuilder.Entity<Dish>()
                .Property(d => d.Name)
                .IsRequired();

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
