using FullCartApi.DataAccess.Data;
using FullCartApi.Interfaces;
using FullCartApi.Models;
using Microsoft.EntityFrameworkCore;

namespace FullCartApi.Services
{
    public class BrandService : IBrandService
    {
        public List<Brand> GetAllBrands(ApplicationDbContext _db)
        {
            return _db.Brands.OrderBy(x => x.Id).ToList();
        }

        public bool SubmitBrand(ApplicationDbContext _db, Brand model)
        {
            if (model.Id > 0)
            {
                Brand? brand = _db.Brands.FirstOrDefault(x => x.Id == model.Id);
                if (brand != null)
                {
                    brand.BrandName = model.BrandName;
                    _db.Entry(brand).State = EntityState.Modified;
                    _db.SaveChanges();
                    return true;
                }
            }
            else
            {
                _db.Brands.Add(model);
                _db.SaveChanges();
                return true;
            }

            return false;
        }

        public bool DeleteBrandById(ApplicationDbContext _db, int id)
        {
            var dataForDelete = _db.Brands
                                   .FirstOrDefault(x => x.Id == id);

            if (dataForDelete != null)
            {
                _db.Brands.Remove(dataForDelete);
                _db.SaveChanges();
                return true;
            }

            return false;
        }
    }
}
