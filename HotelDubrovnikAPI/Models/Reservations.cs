﻿using System.ComponentModel.DataAnnotations;
using HotelDubrovnikAPI.Validations;

namespace HotelDubrovnikAPI.Models
{
    public class Reservations
    {
        [Key]
        public int Reservation_id { get; set; }
        public int Room_id { get; set; }

        [DateValidation]
        public DateTime From_date { get; set; }

        [DateValidation]
        public DateTime To_date { get; set; }
        public string PaymentMethod { get; set; } = string.Empty;
        public string FullName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;

        [IdNumberValidation]
        public int IdentificationNr { get; set; }
        public int PhoneNumber { get; set; }
    }
}
