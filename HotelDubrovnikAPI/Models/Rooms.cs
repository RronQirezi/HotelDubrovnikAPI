using System.ComponentModel.DataAnnotations;

namespace HotelDubrovnikAPI.Models
{
    public class Rooms
    {
        [Key]
        public int Room_Id { get; set; }
        public int Phone_Number { get; set; }
        public int Room_Number { get; set; }
        public string Description { get; set; } = String.Empty;
        public int Booked { get; set; }
        public string Room_Type { get; set; } = String.Empty;
        public string Room_Photo { get; set; } = String.Empty;
        public int Room_Price { get; set; }
        public int Room_Capacity { get; set; }
    }
}
