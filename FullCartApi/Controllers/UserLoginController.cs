using FullCartApi.DataAccess.Data;
using FullCartApi.Interfaces;
using FullCartApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace FullCartApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserLoginController : ControllerBase
    {
        private readonly IUserLoginService _UserLoginService;
        private readonly ApplicationDbContext _db;

        public UserLoginController(IUserLoginService UserLoginService, ApplicationDbContext db)
        {
            _UserLoginService = UserLoginService;
            _db = db;
        }

        [HttpPost("login")]
        public IActionResult UserLogin(UserLoginModel model)
        {
            try
            {
                UserResponseModel data = _UserLoginService.UserLogin(_db, model);

                if (data != null)
                {
                    var response = new
                    {
                        IsExecuted = true,
                        Data = data,
                        Message = data.Message
                    };
                    return Ok(response);
                }
                else
                {
                    var response = new
                    {
                        IsExecuted = false,
                        Data = "",
                        Message = "No user found"
                    };
                    return Ok(response);
                }
            }
            catch (Exception ex)
            {
                var response = new
                {
                    IsExecuted = false,
                    Data = "",
                    Message = ex.InnerException != null ? ex.InnerException.Message : ex.Message
                    
                };
                return Ok(response);
            }
        }

    }
}
