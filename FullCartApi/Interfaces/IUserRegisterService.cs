using FullCartApi.DataAccess.Data;
using FullCartApi.Models;
using Microsoft.EntityFrameworkCore;

namespace FullCartApi.Interfaces
{
    public interface IUserRegisterService
    {
        bool UserRegister(ApplicationDbContext _db, UserMaster model);
        List<UserRole> GetAllRole(ApplicationDbContext _db);
    }
}
