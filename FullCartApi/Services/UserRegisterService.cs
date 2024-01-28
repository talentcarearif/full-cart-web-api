using FullCartApi.DataAccess.Data;
using FullCartApi.Interfaces;
using FullCartApi.Models;
using Microsoft.EntityFrameworkCore;

namespace FullCartApi.Services
{
    public class UserRegisterService : IUserRegisterService
    {
        public bool UserRegister(ApplicationDbContext _db, UserMaster model)
        {
            UserMaster? getUserInfo = _db.UserMasters
                                         .FirstOrDefault(x => x.Email == model.Email);
            if (getUserInfo != null)
            {
                return false;
            }

            model.CreateDate = DateTime.Now;
            _db.UserMasters.Add(model);
            _db.SaveChanges();
            return true;
        }

        public List<UserRole> GetAllRole(ApplicationDbContext _db)
        {
            return _db.UserRoles.ToList();
        }
    }
}
