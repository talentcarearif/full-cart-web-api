namespace FullCartApi.Models
{
    public class UserLoginModel
    {
        public required string UserEmail { get; set; }
        public required string Password { get; set; }
    }
}
