using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HotelDubrovnikAPI.Models;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;

namespace HotelDubrovnikAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservationsController : ControllerBase
    {
        private readonly DataContext _context;

        public ReservationsController(DataContext context)
        {
            _context = context;
        }

        // GET: api/Reservations
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Reservations>>> GetReservations()
        {
            return await _context.Reservations.Include(r => r.Room_id).ToListAsync();
        }

        // GET: api/Reservations/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Reservations>> GetReservation(int id)
        {
            var reservation = await _context.Reservations.Include(r => r.Room_id)
                                                         .FirstOrDefaultAsync(r => r.Reservation_id == id);

            if (reservation == null)
            {
                return NotFound();
            }

            return reservation;
        }

        // POST: api/Reservations
        [HttpPost]
        public async Task<ActionResult<Reservations>> PostReservation(Reservations reservation)
        {
            // Check if room exists
            var room = await _context.Rooms.FindAsync(reservation.Room_id);

            if (room == null)
            {
                return NotFound("Room not found");
            }

            // Check if there is any overlapping reservation for the same room
            var overlappingReservation = await _context.Reservations
                .Where(r => r.Room_id == reservation.Room_id &&
                            r.From_date < reservation.To_date && r.To_date > reservation.From_date)
                .FirstOrDefaultAsync();

            if (overlappingReservation != null)
            {
                return BadRequest("Room is already reserved for the selected date range.");
            }

            // Mark the room as booked (optional)
            room.Booked = 1; // You might need more sophisticated room availability management
            _context.Rooms.Update(room);

            // Save the reservation
            _context.Reservations.Add(reservation);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetReservation", new { id = reservation.Reservation_id }, reservation);
        }

        // PUT: api/Reservations/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutReservation(int id, Reservations reservation)
        {
            if (id != reservation.Reservation_id)
            {
                return BadRequest();
            }

            _context.Entry(reservation).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ReservationExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // DELETE: api/Reservations/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReservation(int id)
        {
            var reservation = await _context.Reservations.FindAsync(id);
            if (reservation == null)
            {
                return NotFound();
            }

            var room = await _context.Rooms.FindAsync(reservation.Room_id);
            if (room != null)
            {
                room.Booked = 0; // Mark the room as available again
                _context.Rooms.Update(room);
            }

            _context.Reservations.Remove(reservation);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ReservationExists(int id)
        {
            return _context.Reservations.Any(e => e.Reservation_id == id);
        }
    }
}
