using FullCartApi.DataAccess.Data;
using FullCartApi.Models;
using FullCartApi.Models.ViewModel;
using Microsoft.EntityFrameworkCore;

namespace FullCartApi.Interfaces
{
    public interface ICartOrderService
    {
        dynamic? GetUserShoppingCartDetails(ApplicationDbContext _db, string loginUser);
        bool SubmitOrderDetails(ApplicationDbContext _db, string loginUser);
        bool CancelCustomerOrder(ApplicationDbContext _db, int id);

        List<OrderHeadersVM> GetAllOrderDetails(ApplicationDbContext _db);
        List<OrderHeadersVM> GetCustomerOrderDetails(ApplicationDbContext _db, string loginUser);

    }
}
