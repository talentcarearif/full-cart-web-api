using FullCartApi.Models;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace FullCartApi.DataAccess.Data
{
    public class ApplicationDbContext: DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<UserMaster> UserMasters { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<ShoppingCart> ShoppingCarts { get; set; }
        public DbSet<OrderHeader> OrderHeaders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<UserRole>().HasData(
                new UserRole { Id = 1, RoleName = "Admin"},
                new UserRole { Id = 2, RoleName = "Customer"}
            );

            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1, CategoryName = "Food" },
                new Category { Id = 2, CategoryName = "Electronics" }
            );

            modelBuilder.Entity<Brand>().HasData(
                new Brand { Id = 1, BrandName = "Square" },
                new Brand { Id = 2, BrandName = "Aci" }
            );

            modelBuilder.Entity<UserMaster>().HasData(
                new UserMaster { 
                    Id = 1, 
                    FirstName = "Admin User" ,
                    Password = "admin",
                    Email = "admin@admin.com",
                    Mobile = "01917560935",
                    Address = "Bangladesh",
                    UserType = "Active",
                    UserRoleId = 1,
                    CreateDate = DateTime.Now,
                }
            );

            modelBuilder.Entity<Product>().HasData(
                new Product
                {
                    Id = 1,
                    ProductName = "Honey",
                    Description = "Pure honey. Collected from natural resources",
                    Price = 599,
                    Quantity = 15,
                    CategoryId = 1,
                    BrandId = 1,
                    ImagePath = "https://images.healthshots.com/healthshots/en/uploads/2022/08/02140352/honey-1600x900.jpg"
                },
                 new Product
                 {
                     Id = 2,
                     ProductName = "Black Seed Oil",
                     Description = "Black Seed Oil 500 gram & 1KG bottle",
                     Price = 2500,
                     Quantity = 11,
                     CategoryId = 1,
                     BrandId = 2,
                     ImagePath = "https://as1.ftcdn.net/v2/jpg/00/61/13/74/1000_F_61137473_57HL6gtixhS8ILr3phC2dpLwDg85s71n.jpg"
                 },
                 new Product
                 {
                     Id = 3,
                     ProductName = "Choco Biscuit Blast",
                     Description = "Drink mixed with chocolate and biscuit",
                     Price = 250,
                     Quantity = 10,
                     CategoryId = 1,
                     BrandId = 1,
                     ImagePath = "https://asianfoodnetwork.com/content/dam/afn/global/en/recipes/avocado-milo/AFN_avocado_milo_main_image.jpg.transform/desktop-img/img.jpg"
                 },
                 new Product
                 {
                     Id = 4,
                     ProductName = "Choco fruit",
                     Description = "Chocolate drink with fruit",
                     Price = 250,
                     Quantity = 9,
                     CategoryId = 1,
                     BrandId = 2,
                     ImagePath = "https://asianfoodnetwork.com/content/dam/afn/global/en/recipes/avocado-milo/AFN_avocado_milo_step2.jpg.transform/recipestep-img/img.jpg"
                 },
                 new Product
                 {
                     Id = 5,
                     ProductName = "Grass Tea",
                     Description = "Special tea with lemon and other natural grass",
                     Price = 130,
                     Quantity = 19,
                     CategoryId = 1,
                     BrandId = 2,
                     ImagePath = "https://asianfoodnetwork.com/content/dam/afn/global/en/recipes/bajigur/AFN_bajigur_main_image.jpg.transform/desktop-img/img.jpg"
                 },
                 new Product
                 {
                     Id = 6,
                     ProductName = "Mojito",
                     Description = "Refreshing Drink - mojito",
                     Price = 150,
                     Quantity = 21,
                     CategoryId = 1,
                     BrandId = 1,
                     ImagePath = ""
                 }
                );
        }

    }
}
