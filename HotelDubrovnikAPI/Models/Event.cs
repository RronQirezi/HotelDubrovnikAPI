using System.ComponentModel.DataAnnotations;

namespace HotelDubrovnikAPI.Models
{
    public class Event
    {
        [Key]
        public int EventId { get; set; }
        public string? EventTitle { get; set; } = String.Empty;
        public DateTime EventDateFrom { get; set; } 
        public DateTime EventDateTo { get; set; }
        public string? EventImage { get; set; }
        public string? EventDescription { get; set; } = String.Empty;
        public string? EventURL { get; set; } = String.Empty;
    }
}
