using FullCartApi.DataAccess.Data;
using FullCartApi.Models;
using Microsoft.EntityFrameworkCore;

namespace FullCartApi.Interfaces
{
    public interface IUserLoginService
    {
        UserResponseModel UserLogin(ApplicationDbContext _db, UserLoginModel model);
    }
}
