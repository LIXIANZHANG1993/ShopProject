using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using ShopProject.Models.Shop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopProject.Data
{
    public class ShopDbContext : DbContext
    {
        public ShopDbContext()
        {
        }

        public ShopDbContext(DbContextOptions<ShopDbContext> options)
            : base(options)
        {
        }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Purchase> Purchases { get; set; }

        public DbSet<ProductType> productTypes { get; set; }

        public DbSet<Cart> Carts { get; set; }
        public DbSet<ChangePassWord> ChangePassWords { get; set; }



        /*protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=(localdb)\MSSQLLocalDB;Database=ShopProjectDb;Trusted_Connection=True;Integrated Security=true;");
            base.OnConfiguring(optionsBuilder);

        }*/

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>().HasData(

               new Product { Id = 1, Name = "White Short Jacket", Model = 2021052200001, Price = 900, ImgUrl = "~/images/clothes/clothes1.jpg", TypeId = 1, CreationDate = DateTime.Now, Inventory = 99 },
               new Product { Id = 2, Name = "White Short vest ", Model = 2021052200002, Price = 500, ImgUrl = "~/images/clothes/clothes2.jpg", TypeId = 1, CreationDate = DateTime.Now, Inventory = 99 },
               new Product { Id = 3, Name = "White Lace vest", Model = 2021052200003, Price = 500, ImgUrl = "~/images/clothes/clothes3.jpg", TypeId = 1, CreationDate = DateTime.Now, Inventory = 99 },
               new Product { Id = 4, Name = "Sleeveless vest", Model = 2021052200004, Price = 700, ImgUrl = "~/images/clothes/clothes4.jpg", TypeId = 1, CreationDate = DateTime.Now, Inventory = 99 },
               new Product { Id = 5, Name = "Gray Lace pants", Model = 2021052200005, Price = 1200, ImgUrl = "~/images/bottom/bottom1.jpg", TypeId = 2, CreationDate = DateTime.Now, Inventory = 99 },
               new Product { Id = 6, Name = "Black Satin Pants", Model = 2021052200006, Price = 1200, ImgUrl = "~/images/bottom/bottom2.jpg", TypeId = 2, CreationDate = DateTime.Now, Inventory = 99 },
               new Product { Id = 7, Name = "Yellow Plaid Pants", Model = 2021052200007, Price = 1100, ImgUrl = "~/images/bottom/bottom3.jpg", TypeId = 2, CreationDate = DateTime.Now, Inventory = 99 },
               new Product { Id = 8, Name = "Five-Point Jeans", Model = 2021052200008, Price = 1200, ImgUrl = "~/images/bottom/bottom4.jpg", TypeId = 2, CreationDate = DateTime.Now, Inventory = 99 },
               new Product { Id = 9, Name = "Red High Heels", Model = 2021052200009, Price = 900, ImgUrl = "~/images/shoes/shoes1.jpg", TypeId = 3, CreationDate = DateTime.Now, Inventory = 99 },
               new Product { Id = 10, Name = "White High Heels", Model = 2021052200010, Price = 900, ImgUrl = "~/images/shoes/shoes2.jpg", TypeId = 3, CreationDate = DateTime.Now, Inventory = 99 },
               new Product { Id = 11, Name = "Green High Heels", Model = 2021052200011, Price = 900, ImgUrl = "~/images/shoes/shoes3.jpg", TypeId = 3, CreationDate = DateTime.Now, Inventory = 99 },
               new Product { Id = 12, Name = "White Sneakers", Model = 2021052200012, Price = 900, ImgUrl = "~/images/shoes/shoes4.jpg", TypeId = 3, CreationDate = DateTime.Now, Inventory = 99 },
               new Product { Id = 13, Name = "Gold Retro Watch", Model = 2021052200013, Price = 600, ImgUrl = "~/images/accessories/accessories1.jpg", TypeId = 4, CreationDate = DateTime.Now, Inventory = 99 },
               new Product { Id = 14, Name = "Fimbrilla Earings", Model = 2021052200014, Price = 500, ImgUrl = "~/images/accessories/accessories2.jpg", TypeId = 4, CreationDate = DateTime.Now, Inventory = 99 },
               new Product { Id = 15, Name = "Leather Handbag", Model = 2021052200015, Price = 800, ImgUrl = "~/images/accessories/accessories3.jpg", TypeId = 4, CreationDate = DateTime.Now, Inventory = 99 },
               new Product { Id = 16, Name = "Retro Glasses", Model = 2021052200016, Price = 800, ImgUrl = "~/images/accessories/accessories4.jpg", TypeId = 4, CreationDate = DateTime.Now, Inventory = 99 }


               );

            modelBuilder.Entity<Customer>().HasData(

                new Customer { Id = 1, Name = "Admin", Email = "Admin@gmail.com", Password = "123", Admin = true, Birthday = DateTime.Now, Address = "333桃園市龜山區德明路5號", Phone = "0987654321", Gender = "Female" },
                new Customer { Id = 2, Name = "User", Email = "User@gmail.com", Password = "123", Admin = false, Birthday = DateTime.Now, Address = "333桃園市龜山區德明路5號", Phone = "0912345678", Gender = "Male" }

                );


            modelBuilder.Entity<ProductType>().HasData(

                new ProductType { Id = 1, Name = "Top" },
                new ProductType { Id = 2, Name = "Bottom" },
                new ProductType { Id = 3, Name = "Shoes" },
                new ProductType { Id = 4, Name = "Accessories" }
                );







        }
    }
}

