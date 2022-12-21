using System.ComponentModel.DataAnnotations;

namespace HotelDubrovnikAPI.Models
{
    public class Event
    {
        [Key]
        public int event_id { get; set; }
        public string? event_title { get; set; } = String.Empty;
        public DateTime from_date { get; set; } 
        public DateTime to_date { get; set; }
        public string? event_img { get; set; }
        public string? description { get; set; } = String.Empty;
        public string? event_url { get; set; } = String.Empty;
    }
}
