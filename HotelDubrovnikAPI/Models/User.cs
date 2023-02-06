using System.ComponentModel.DataAnnotations.Schema;

namespace HotelDubrovnikAPI.Models
{
    public class User
    {
        public string Username { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        [NotMapped]
        public DateTime Created_At { get; set; }
        
    }
}
