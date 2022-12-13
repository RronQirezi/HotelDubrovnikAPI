using Microsoft.EntityFrameworkCore;

namespace HotelDubrovnikAPI.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions options) : base(options) { }

        public DbSet<Models.Event> Events { get; set; }
        public DbSet<Models.Rooms> Rooms { get; set; }
    }
}
