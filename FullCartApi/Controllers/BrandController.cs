using FullCartApi.DataAccess.Data;
using FullCartApi.Interfaces;
using FullCartApi.Models;
using FullCartApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace FullCartApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BrandController : ControllerBase
    {
        private readonly IBrandService _BrandService;
        private readonly ApplicationDbContext _db;

        public BrandController(IBrandService BrandService, ApplicationDbContext db)
        {
            _BrandService = BrandService;
            _db = db;
        }

        [HttpGet("get-all")]
        public IActionResult GetAllBrands()
        {
            try
            {
                List<Brand> data = _BrandService.GetAllBrands(_db);

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

        [HttpPost("submit")]
        public IActionResult SubmitBrand(Brand model)
        {
            using (var dbTransaction = _db.Database.BeginTransaction())
            {
                try
                {
                    bool data = _BrandService.SubmitBrand(_db, model);

                    if (data)
                    {
                        dbTransaction.Commit();
                        var response = new
                        {
                            IsExecuted = true,
                            Data = "",
                            Message = model.Id > 0 ? "Data modified successfully" : "Data inserted successfully"
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

        [HttpDelete("delete/{id}")]
        public IActionResult DeleteBrandById(int id)
        {
            using (var dbTransaction = _db.Database.BeginTransaction())
            {
                try
                {
                    bool data = _BrandService.DeleteBrandById(_db, id);

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
    }
}
