using FullCartApi.DataAccess.Data;
using FullCartApi.Interfaces;
using FullCartApi.Models;
using FullCartApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace FullCartApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _CategoryService;
        private readonly ApplicationDbContext _db;

        public CategoryController(ICategoryService CategoryService, ApplicationDbContext db)
        {
            _CategoryService = CategoryService;
            _db = db;
        }

        [HttpGet("get-all")]
        public IActionResult GetAllCategories()
        {
            try
            {
                List<Category> data = _CategoryService.GetAllCategories(_db);

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
        public IActionResult SubmitProducts(Category model)
        {
            using (var dbTransaction = _db.Database.BeginTransaction())
            {
                try
                {
                    bool data = _CategoryService.SubmitCategory(_db, model);

                    if (data)
                    {
                        dbTransaction.Commit();
                        var response = new
                        {
                            IsExecuted = true,
                            Data = "",
                            Message = model.Id > 0? "Data modified successfully" :"Data inserted successfully"
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
        public IActionResult DeleteCategoryById(int id)
        {
            using (var dbTransaction = _db.Database.BeginTransaction())
            {
                try
                {
                    bool data = _CategoryService.DeleteCategoryById(_db, id);

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
