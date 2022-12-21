using System.ComponentModel.DataAnnotations;

namespace HotelDubrovnikAPI.Models
{
    public class Reservations
    {
        [Key]
        public int reservation_id { get; set; }
        public int room_id { get; set; }
        public DateTime from_date { get; set; }
        public DateTime to_date { get; set; }
        public string payment { get; set; } = string.Empty;
        public int user_id { get; set; }
        public string full_name { get; set; } = string.Empty;
        public int identification { get; set; }
        public int phone_number { get; set; }
    }
}
