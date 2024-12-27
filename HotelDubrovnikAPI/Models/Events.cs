using System.ComponentModel.DataAnnotations;
using HotelDubrovnikAPI.Validations;


namespace HotelDubrovnikAPI.Models
{
    public class Events
    {
        [Key]
        public int Event_id { get; set; }
        public string? Event_title { get; set; } = String.Empty;

        [DateValidation]
        public DateTime FromDate { get; set; }

        [DateValidation]
        public DateTime ToDate { get; set; }
        public string? Event_img { get; set; }
        public string? Description { get; set; } = String.Empty;
        public string? Event_url { get; set; } = String.Empty;
    }
}
