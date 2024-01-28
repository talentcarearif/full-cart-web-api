using FullCartApi.DataAccess.Data;
using FullCartApi.Models;
using Microsoft.EntityFrameworkCore;

namespace FullCartApi.Interfaces
{
    public interface ICategoryService
    {
        List<Category> GetAllCategories(ApplicationDbContext _db);
        bool DeleteCategoryById(ApplicationDbContext _db, int id);
        bool SubmitCategory(ApplicationDbContext _db, Category model);
    }
}
