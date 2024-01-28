using FullCartApi.DataAccess.Data;
using FullCartApi.Interfaces;
using FullCartApi.Models;
using FullCartApi.Models.ViewModel;
using Microsoft.EntityFrameworkCore;

namespace FullCartApi.Services
{
    public class ProductService : IProductService
    {        
        public bool SubmitProducts(ApplicationDbContext _db, Product model)
        {
            if (model.Id > 0)
            {
                Product? productData = _db.Products.FirstOrDefault(x => x.Id == model.Id);

                if (productData != null)
                {
                    productData.ProductName = model.ProductName;
                    productData.Description = model.Description;
                    productData.Price = model.Price;
                    productData.Quantity = model.Quantity;
                    productData.CategoryId = model.CategoryId;
                    productData.BrandId = model.BrandId;
                    productData.ImagePath = model.ImagePath;

                    _db.Entry(productData).State = EntityState.Modified;
                    _db.SaveChanges();
                    return true;
                }
            }
            else
            {
                _db.Products.Add(model);
                _db.SaveChanges();
                return true;
            }

            return false;
        }

        public bool SubmitProductsFromExcel(ApplicationDbContext _db, List<Product> model)
        {
            _db.Products.AddRange(model);
            _db.SaveChanges();
            return true;
        }

        public List<Product> GetAllProducts(ApplicationDbContext _db)
        {
            return _db.Products
                      .AsQueryable()
                      .Select(x => new Product
                      {
                          Id = x.Id,
                          ProductName = x.ProductName,
                          Description = x.Description,
                          Price = x.Price,
                          Quantity = x.Quantity,
                          CategoryId = x.CategoryId,
                          Category = _db.Categories.FirstOrDefault(y => y.Id == x.CategoryId),
                          BrandId = x.BrandId,
                          Brand = _db.Brands.FirstOrDefault(y => y.Id == x.BrandId),
                          ImagePath = x.ImagePath,
                          CartCount = 0,
                      })
                      .ToList();
        }

        public bool DeleteProductById(ApplicationDbContext _db, int id)
        {
            var dataForDelete = _db.Products
                                   .FirstOrDefault(x => x.Id == id);

            if (dataForDelete != null)
            {
                _db.Products.Remove(dataForDelete);
                _db.SaveChanges();
                return true;
            }

            return false;
        }

        public bool AddToCartAsPerUser(ApplicationDbContext _db, GetProductAndUserVM model)
        {
            string loginUser = model.LoginUser;
            int productId = model.ProductId;

            UserMaster? getUser = _db.UserMasters.FirstOrDefault(x => x.Email == loginUser);

            if (getUser == null)
            {
                return false;
            }
 
            ShoppingCart? cart = _db.ShoppingCarts.FirstOrDefault(x => x.UserMasterId == getUser.Id && x.ProductId == productId);

            if (cart != null)
            {
                cart.Count += 1;
                _db.Entry(cart).State = EntityState.Modified;
            }
            else
            {
                ShoppingCart newShoppingObj = new()
                {
                    UserMasterId = getUser.Id,
                    ProductId = productId,
                    Count = 1
                };

                _db.ShoppingCarts.Add(newShoppingObj);
            }
            _db.SaveChanges();
            return true;
        }

        public bool RemoveFromCartAsPerUser(ApplicationDbContext _db, GetProductAndUserVM model)
        {
            string loginUser = model.LoginUser;
            int productId = model.ProductId;

            UserMaster? getUser = _db.UserMasters.FirstOrDefault(x => x.Email == loginUser);

            if (getUser == null)
            {
                return false;
            }

            ShoppingCart? cart = _db.ShoppingCarts.FirstOrDefault(x => x.UserMasterId == getUser.Id && x.ProductId == productId);

            if (cart != null && cart.Count > 0)
            {
                cart.Count -= 1;
                _db.Entry(cart).State = EntityState.Modified;
                _db.SaveChanges();
            }
            return true;
        }

        public int GetTotalCartCount(ApplicationDbContext _db, string loginUser)
        {
            UserMaster? getUser = _db.UserMasters.FirstOrDefault(x => x.Email == loginUser);

            if (getUser == null)
            {
                return 0;
            }
            else
            {
                return _db.ShoppingCarts.Where(x => x.UserMasterId == getUser.Id).Sum(x => x.Count);
            }
        }
    }
}
