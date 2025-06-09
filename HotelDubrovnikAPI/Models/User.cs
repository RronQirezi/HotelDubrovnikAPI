using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HotelDubrovnikAPI.Models
{
    public class User
    {
        [Key]
        public int User_ID { get; set; }

        public string Username { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public string Role = "admin";
        [NotMapped]
        public string Password { get; set; } = string.Empty;  // For input only

        
    }
}
