namespace FullCartApi.Models.ViewModel
{
    public class OrderHeadersVM : OrderHeader
    {
        public List<OrderDetail>? OrderDetails { get; set; }
        public string? UserName { get; set; }
    }
}
