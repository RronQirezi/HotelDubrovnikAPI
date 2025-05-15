using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace HotelDubrovnikAPI.Models
{
    public class Reservations
    {
        [Key]
        public int Reservation_id { get; set; }

        public int Room_id { get; set; }

        public DateTime From_date { get; set; }

        public DateTime To_date { get; set; }

        public string PaymentMethod { get; set; } = string.Empty;

        public string FullName { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public string IdentificationNr { get; set; } = string.Empty;

        public string PhoneNumber { get; set; } = string.Empty;
    }
}
