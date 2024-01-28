using FullCartApi.DataAccess.Data;
using FullCartApi.Models;
using FullCartApi.Models.ViewModel;
using Microsoft.EntityFrameworkCore;

namespace FullCartApi.Interfaces
{
    public interface IProductService
    {
        bool SubmitProducts(ApplicationDbContext _db, Product model);
        bool SubmitProductsFromExcel(ApplicationDbContext _db, List<Product> model);
        List<Product> GetAllProducts(ApplicationDbContext _db);
        bool DeleteProductById(ApplicationDbContext _db, int id);
        bool AddToCartAsPerUser(ApplicationDbContext _db, GetProductAndUserVM model);
        bool RemoveFromCartAsPerUser(ApplicationDbContext _db, GetProductAndUserVM model);
        int GetTotalCartCount(ApplicationDbContext _db, string loginUser);

    }
}
