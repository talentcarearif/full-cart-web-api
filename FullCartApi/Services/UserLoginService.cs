using FullCartApi.DataAccess.Data;
using FullCartApi.Interfaces;
using FullCartApi.Models;
using Microsoft.EntityFrameworkCore;

namespace FullCartApi.Services
{
    public class UserLoginService : IUserLoginService
    {
        public UserResponseModel UserLogin(ApplicationDbContext _db, UserLoginModel model)
        {
            UserMaster? getUserInfo = _db.UserMasters
                                        .FirstOrDefault(x => x.Email == model.UserEmail &&
                                                             x.Password == model.Password &&
                                                             x.UserType == "Active");
            if (getUserInfo != null)
            {
                var userInfo = new
                {
                    UserName = getUserInfo.FirstName,
                    UserEmail = getUserInfo.Email,
                    UserMobile = getUserInfo.Mobile,
                    UserAddress = getUserInfo.Address,
                    UserRole = _db.UserRoles.FirstOrDefault(x => x.Id == getUserInfo.UserRoleId)?.RoleName
                };

                var response = new UserResponseModel
                {
                    IsAuthenticated = true,
                    UserInformation = userInfo,
                    Token = "",
                    Message = "User login successfully"
                };

                return response;
            }
            else
            {
                var response = new UserResponseModel
                {
                    IsAuthenticated = false,
                    UserInformation = "",
                    Token = "",
                    Message = "Email or password does not match"
                };

                return response;
            }
        }
    }
}
