using FullCartApi.DataAccess.Data;
using FullCartApi.Models;
using Microsoft.EntityFrameworkCore;

namespace FullCartApi.Interfaces
{
    public interface IBrandService
    {
        List<Brand> GetAllBrands(ApplicationDbContext _db);
        bool SubmitBrand(ApplicationDbContext _db, Brand model);
        bool DeleteBrandById(ApplicationDbContext _db, int id);
    }
}
