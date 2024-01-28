using FullCartApi.DataAccess.Data;
using FullCartApi.Interfaces;
using FullCartApi.Models;
using FullCartApi.Models.ViewModel;
using FullCartApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace FullCartApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CartOrderController : ControllerBase
    {
        private readonly ICartOrderService _CartOrderService;
        private readonly ApplicationDbContext _db;

        public CartOrderController(ICartOrderService CartOrderService, ApplicationDbContext db)
        {
            _CartOrderService = CartOrderService;
            _db = db;
        }

        [HttpGet("shopping-cart/get-all/{loginUser}")]
        public IActionResult GetUserShoppingCartDetails(string loginUser)
        {
            try
            {
                var data = _CartOrderService.GetUserShoppingCartDetails(_db, loginUser);

                if (data != null)
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

        [HttpPost("place-order/{loginUser}")]
        public IActionResult SubmitOrderDetails(string loginUser)
        {
            using (var dbTransaction = _db.Database.BeginTransaction())
            {
                try
                {
                    bool data = _CartOrderService.SubmitOrderDetails(_db, loginUser);

                    if (data)
                    {
                        dbTransaction.Commit();
                        var response = new
                        {
                            IsExecuted = true,
                            Data = "",
                            Message = "Order placed successfully"
                        };
                        return Ok(response);
                    }
                    else
                    {
                        dbTransaction.Rollback();
                        var response = new
                        {
                            IsExecuted = false,
                            Data = "",
                            Message = "Error to order"
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
                        Data = "",
                        Message = ex.InnerException != null ? ex.InnerException.Message : ex.Message
                    };
                    return Ok(response);
                }
            }
        }

        [HttpGet("get-order-details")]
        public IActionResult GetAllOrderDetails()
        {
            try
            {
                var data = _CartOrderService.GetAllOrderDetails(_db);

                if (data != null)
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

        [HttpGet("get-order-details/customer/{loginUser}")]
        public IActionResult GetCustomerOrderDetails(string loginUser)
        {
            try
            {
                var data = _CartOrderService.GetCustomerOrderDetails(_db, loginUser);

                if (data != null)
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

        [HttpDelete("cancel-order/{id}")]
        public IActionResult CancelCustomerOrder(int id)
        {
            using (var dbTransaction = _db.Database.BeginTransaction())
            {
                try
                {
                    bool data = _CartOrderService.CancelCustomerOrder(_db, id);

                    if (data)
                    {
                        dbTransaction.Commit();
                        var response = new
                        {
                            IsExecuted = true,
                            Data = "",
                            Message = "Data cancelled successfully"
                        };
                        return Ok(response);
                    }
                    else
                    {
                        dbTransaction.Rollback();
                        var response = new
                        {
                            IsExecuted = false,
                            Data = "",
                            Message = "Error to cancel data"
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
                        Data = "",
                        Message = ex.InnerException != null ? ex.InnerException.Message : ex.Message
                    };
                    return Ok(response);
                }
            }
        }
    }
}
