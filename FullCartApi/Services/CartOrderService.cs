using FullCartApi.DataAccess.Data;
using FullCartApi.Interfaces;
using FullCartApi.Models;
using FullCartApi.Models.ViewModel;
using Microsoft.EntityFrameworkCore;

namespace FullCartApi.Services
{
    public class CartOrderService : ICartOrderService
    {
        public dynamic? GetUserShoppingCartDetails(ApplicationDbContext _db, string loginUser)
        {
            UserMaster? userInfo = _db.UserMasters.FirstOrDefault(x => x.Email == loginUser);

            if (userInfo != null)
            {
                List<ShoppingCart> shoppingCart = _db.ShoppingCarts
                                                     .Where(x => x.UserMasterId == userInfo.Id)
                                                     .Select(x => new ShoppingCart
                                                     {
                                                         Id = x.Id,
                                                         ProductId = x.ProductId,
                                                         Count = x.Count,
                                                         UserMasterId = x.UserMasterId,
                                                         UserMaster = x.UserMaster,
                                                         Product = (_db.Products.FirstOrDefault(y => y.Id == x.ProductId)),
                                                         Price = x.Price
                                                     })
                                                     .ToList();

                foreach(var cart in shoppingCart )
                {
                    cart.Price = cart.Product != null? (cart.Product.Price * cart.Count) : 0;
                }

                return shoppingCart;
            }

            return null;
        }    
        
        public bool SubmitOrderDetails(ApplicationDbContext _db, string loginUser)
        {
            UserMaster? userInfo = _db.UserMasters.FirstOrDefault(x => x.Email == loginUser);

            if (userInfo != null)
            {
                IEnumerable<ShoppingCart> shoppingCart = _db.ShoppingCarts
                                                            .Where(x => x.UserMasterId == userInfo.Id);


                OrderHeader newHeaderObj = new()
                {
                    Id = 0,
                    UserMasterId = userInfo.Id,
                    OrderDate = DateTime.Now,
                    OrderTotal = shoppingCart.Count(),
                    OrderStatus = "Pending",
                    PaymentStatus = "Pending"
                };
                _db.OrderHeaders.Add(newHeaderObj);
                _db.SaveChanges();  

                foreach (var item in shoppingCart)
                {
                    OrderDetail newOrderDetailsObj = new()
                    {
                        OrderHeaderId = newHeaderObj.Id,
                        Count = item.Count,
                        Price = _db.Products.FirstOrDefault(x => x.Id == item.ProductId).Price,
                        ProductId = item.ProductId
                    };

                    _db.OrderDetails.Add(newOrderDetailsObj);
                }

               
                _db.ShoppingCarts.RemoveRange(shoppingCart);
            }

            _db.SaveChanges();
            return true;
        }

        public List<OrderHeadersVM> GetAllOrderDetails(ApplicationDbContext _db)
        {
           var orderDtl = _db.OrderHeaders
                             .Select(x => new OrderHeadersVM
                             {
                                 Id = x.Id,
                                 UserMasterId = x.UserMasterId,
                                 UserMaster = _db.UserMasters.FirstOrDefault(y => y.Id == x.UserMasterId),
                                 UserName = _db.UserMasters.FirstOrDefault(y => y.Id == x.UserMasterId).FirstName,
                                 OrderDate = x.OrderDate,
                                 OrderTotal = x.OrderTotal,
                                 OrderStatus = x.OrderStatus,
                                 OrderDetails = _db.OrderDetails
                                                   .Where(y => y.OrderHeaderId == x.Id)
                                                   .Select(z => new OrderDetail
                                                   {
                                                       Id = z.Id,
                                                       ProductId = z.ProductId,
                                                       Product = _db.Products.FirstOrDefault(a => a.Id == z.ProductId),
                                                       Count = z.Count,
                                                       Price = z.Price,
                                                   })
                                                   .ToList()
                             })
                             .OrderByDescending(x => x.OrderDate)
                             .ToList();

            return orderDtl;
        }

        public List<OrderHeadersVM> GetCustomerOrderDetails(ApplicationDbContext _db, string loginUser)
        {
            UserMaster? userInfo = _db.UserMasters.FirstOrDefault(x => x.Email == loginUser);

            var orderDtl = _db.OrderHeaders
                              .Where(x => x.UserMasterId == userInfo.Id)
                              .Select(x => new OrderHeadersVM
                              {
                                  Id = x.Id,
                                  UserMasterId = x.UserMasterId,
                                  UserMaster = _db.UserMasters.FirstOrDefault(y => y.Id == x.UserMasterId),
                                  UserName = _db.UserMasters.FirstOrDefault(y => y.Id == x.UserMasterId).FirstName,
                                  OrderDate = x.OrderDate,
                                  OrderTotal = x.OrderTotal,
                                  OrderStatus = x.OrderStatus,
                                  OrderDetails = _db.OrderDetails
                                                      .Where(y => y.OrderHeaderId == x.Id)
                                                      .Select(z => new OrderDetail
                                                      {
                                                          Id = z.Id,
                                                          ProductId = z.ProductId,
                                                          Product = _db.Products.FirstOrDefault(a => a.Id == z.ProductId),
                                                          Count = z.Count,
                                                          Price = z.Price,
                                                      })
                                                      .ToList()
                              })
                              .OrderByDescending(x => x.OrderDate)
                              .ToList();

            return orderDtl;
        }

        public bool CancelCustomerOrder(ApplicationDbContext _db, int id)
        {
            OrderHeader? orderHead = _db.OrderHeaders.FirstOrDefault(x => x.Id == id);

            if (orderHead == null)
            {
                return false;
            }

            orderHead.OrderStatus = "Cancelled";
            _db.Entry(orderHead).State = EntityState.Modified;
            _db.SaveChanges();
            return true;
        }
    }
}
