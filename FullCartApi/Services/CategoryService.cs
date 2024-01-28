using FullCartApi.DataAccess.Data;
using FullCartApi.Interfaces;
using FullCartApi.Models;
using Microsoft.EntityFrameworkCore;

namespace FullCartApi.Services
{
    public class CategoryService : ICategoryService
    {
        public List<Category> GetAllCategories(ApplicationDbContext _db)
        {
            return _db.Categories.OrderBy(x => x.Id).ToList();
        }

        public bool DeleteCategoryById(ApplicationDbContext _db, int id)
        {
            var dataForDelete = _db.Categories
                                   .FirstOrDefault(x => x.Id == id);

            if (dataForDelete != null)
            {
                _db.Categories.Remove(dataForDelete);
                _db.SaveChanges();
                return true;
            }

            return false;
        }

        public bool SubmitCategory(ApplicationDbContext _db, Category model)
        {
            if (model.Id > 0)
            {
                Category? category = _db.Categories.FirstOrDefault(x => x.Id == model.Id);
                if (category != null)
                {
                    category.CategoryName = model.CategoryName;
                    _db.Entry(category).State = EntityState.Modified;
                    _db.SaveChanges();
                    return true;
                }
            }
            else {
                _db.Categories.Add(model);
                _db.SaveChanges();
                return true;
            }

            return false;
        }
    }
}
