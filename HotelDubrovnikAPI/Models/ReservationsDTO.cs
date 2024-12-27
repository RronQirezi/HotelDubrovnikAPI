using System.ComponentModel.DataAnnotations;

namespace HotelDubrovnikAPI.Models
{
    public class ReservationsDTO
    {
        [Key]
        public int Reservation_Id { get; set; }
        public int Room_Id { get; set; }
        public string Description { get; set; } = string.Empty;
        public int Price { get; set; }
        public DateTime From_date { get; set; }
        public DateTime To_date { get; set; }
        public string PaymentMethod { get; set; } = string.Empty;
        public string FullName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public int IdentificationNr { get; set; }
        public int PhoneNumber { get; set; }
    }
}
