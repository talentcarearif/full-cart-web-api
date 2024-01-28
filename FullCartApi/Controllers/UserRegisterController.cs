using FullCartApi.DataAccess.Data;
using FullCartApi.Interfaces;
using FullCartApi.Models;
using FullCartApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace FullCartApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserRegisterController : ControllerBase
    {
        private readonly IUserRegisterService _UserRegisterService;
        private readonly ApplicationDbContext _db;

        public UserRegisterController(IUserRegisterService UserRegisterService, ApplicationDbContext db)
        {
            _UserRegisterService = UserRegisterService;
            _db = db;
        }

        [HttpPost("register/add")]
        public IActionResult UserRegister(UserMaster model)
        {
            using (var dbTransaction = _db.Database.BeginTransaction())
            {
                try
                {
                    bool data = _UserRegisterService.UserRegister(_db, model);

                    if (data)
                    {
                        dbTransaction.Commit();
                        var response = new
                        {
                            IsExecuted = true,
                            Data = data,
                            Message = "New user registration successful"
                        };
                        return Ok(response);
                    }
                    else
                    {
                        dbTransaction.Rollback();
                        var response = new
                        {
                            IsExecuted = false,
                            Data = false,
                            Message = "User already exists"
                        };
                        return Ok(response);
                    }
                }
                catch (Exception ex)
                {
                    dbTransaction.Rollback();
                    var response = new
                    {
                        IsExecuted = false,
                        Data = false,
                        Message = ex.InnerException != null ? ex.InnerException.Message : ex.Message

                    };
                    return Ok(response);
                }
            }
        }


        [HttpGet("user-role/list")]
        public IActionResult GetAllRole()
        {
            try
            {
                List<UserRole> data = _UserRegisterService.GetAllRole(_db);

                if (data?.Count > 0)
                {
                    var response = new
                    {
                        IsExecuted = true,
                        Data = data,
                        Message = "All data"
                    };
                    return Ok(response);
                }
                else
                {
                    var response = new
                    {
                        IsExecuted = false,
                        Data = "",
                        Message = "No data found"
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
