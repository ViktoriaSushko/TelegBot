using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinFormsApp2.Models
{
    public class SkladContext:DbContext
    {
        public DbSet<Category> Categories { get; set; } = null;
        public DbSet<Product> Products { get; set; } = null;
        public DbSet<Cart> Carts { get; set; } = null;
        public SkladContext() { 
            //Database.EnsureDeleted();
            Database.EnsureCreated();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
           object val=optionsBuilder.UseSqlite("Data Source = Sklad.db");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            Category cat1 = new Category { Id = 1, CategoryName = "Продукты" };
            Category cat2 = new Category { Id = 2, CategoryName = "Химия" };
            Category cat3 = new Category { Id = 3, CategoryName = "Піца" };
            Category cat4 = new Category { Id = 4, CategoryName = "Напої" };
            Product pr1 = new Product { Id = 1, Name = "молоко", Units = "шт.", Quantity = 10, Price = 35.00, CategoryId = 1 };
            Product pr2 = new Product { Id = 2, Name = "сметана", Units = "шт.", Quantity = 10, Price = 20.00, CategoryId = 1 };
            Product pr3 = new Product { Id = 3, Name = "шампунь", Units = "шт.", Quantity = 10, Price = 100.00, CategoryId = 2 };
            Product pr4 = new Product { Id = 4, Name = "мыло", Units = "шт.", Quantity = 5, Price = 20.00, CategoryId = 2 };
            modelBuilder.Entity<Category>().HasData(cat1,cat2,cat3,cat4);
            modelBuilder.Entity<Product>().HasData(pr1, pr2,pr3,pr4);
        
        }
    }
}
