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
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly ApplicationDbContext _db;

        public ProductController(IProductService productService, ApplicationDbContext db)
        {
            _productService = productService;
            _db = db;
        }

        [HttpPost("submit")]
        public IActionResult SubmitProducts(Product model)
        {
            using (var dbTransaction = _db.Database.BeginTransaction())
            {
                try
                {
                    bool data = _productService.SubmitProducts(_db, model);

                    if (data)
                    {
                        dbTransaction.Commit();
                        var response = new
                        {
                            IsExecuted = true,
                            Data = "",
                            Message = "Data inserted successfully"
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
                            Message = "Error to insert"
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

        [HttpPost("excel/submit")]
        public IActionResult SubmitProductsFromExcel(List<Product> model)
        {
            using (var dbTransaction = _db.Database.BeginTransaction())
            {
                try
                {
                    bool data = _productService.SubmitProductsFromExcel(_db, model);

                    if (data)
                    {
                        dbTransaction.Commit();
                        var response = new
                        {
                            IsExecuted = true,
                            Data = "",
                            Message = "All Data inserted successfully"
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
                            Message = "Error to insert"
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

        [HttpGet("get-all")]
        public IActionResult GetAllCategories()
        {
            try
            {
                List<Product> data = _productService.GetAllProducts(_db);

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

        [HttpDelete("delete/{id}")]
        public IActionResult DeleteProductById(int id)
        {
            using (var dbTransaction = _db.Database.BeginTransaction())
            {
                try
                {
                    bool data = _productService.DeleteProductById(_db, id);

                    if (data)
                    {
                        dbTransaction.Commit();
                        var response = new
                        {
                            IsExecuted = true,
                            Data = "",
                            Message = "Data deleted successfully"
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
                            Message = "Error to delete data"
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

        [HttpPost("cart/add")]
        public IActionResult AddToCartAsPerUser(GetProductAndUserVM model)
        {
            using (var dbTransaction = _db.Database.BeginTransaction())
            {
                try
                {
                    bool data = _productService.AddToCartAsPerUser(_db, model);

                    if (data)
                    {
                        dbTransaction.Commit();
                        var response = new
                        {
                            IsExecuted = true,
                            Data = "",
                            Message = "Added to cart successfully"
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
                            Message = "Error to add in cart"
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

        [HttpPost("cart/remove")]
        public IActionResult RemoveFromCartAsPerUser(GetProductAndUserVM model)
        {
            using (var dbTransaction = _db.Database.BeginTransaction())
            {
                try
                {
                    bool data = _productService.RemoveFromCartAsPerUser(_db, model);

                    if (data)
                    {
                        dbTransaction.Commit();
                        var response = new
                        {
                            IsExecuted = true,
                            Data = "",
                            Message = "Remove from cart successfully"
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
                            Message = "Error to remove from cart"
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

        [HttpGet("get-cart-count/{loginUser}")]
        public IActionResult GetTotalCartCount(string loginUser)
        {
            try
            {
                int data = _productService.GetTotalCartCount(_db, loginUser);

                if (data > 0)
                {
                    var response = new
                    {
                        IsExecuted = true,
                        Data = data,
                        Message = "Total cart count"
                    };
                    return Ok(response);
                }
                else
                {
                    var response = new
                    {
                        IsExecuted = false,
                        Data = 0,
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
                    Data = 0,
                    Message = ex.InnerException != null ? ex.InnerException.Message : ex.Message

                };
                return Ok(response);
            }
        }
    }
}
