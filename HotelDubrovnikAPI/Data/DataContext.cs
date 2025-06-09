using Microsoft.EntityFrameworkCore;
using HotelDubrovnikAPI.Models;

namespace HotelDubrovnikAPI.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions options) : base(options) { }

        public DbSet<Events> Events { get; set; }
        public DbSet<Rooms> Rooms { get; set; }
        public DbSet<Reservations> Reservations { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
