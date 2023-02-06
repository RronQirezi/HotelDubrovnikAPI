namespace HotelDubrovnikAPI.Models
{
    public class UserDto
    {
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public DateTime? Created_At { get; set; }
        //public int RoleId { get; set; }
    }
}
