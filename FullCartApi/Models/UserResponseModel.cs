namespace FullCartApi.Models
{
    public class UserResponseModel
    {
        public bool IsAuthenticated { get; set; }
        public dynamic? UserInformation { get; set; }
        public string? Token { get; set; }
        public string? Message { get; set; }
    }
}
