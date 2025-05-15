using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HotelDubrovnikAPI.Models;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using System;

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
        [HttpGet("{id}")]
        public async Task<ActionResult<Reservations>> GetReservation(int id)
        {
            var reservation = await _context.Reservations
                .FirstOrDefaultAsync(r => r.Reservation_id == id); // ✅ No Include

            if (reservation == null)
            {
                return NotFound();
            }

            return reservation;
        }

        // GET: api/Reservations/5
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Reservations>>> GetReservations()
        {
            try
            {
                var reservations = await _context.Reservations.ToListAsync();
                return Ok(reservations);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error retrieving reservations: " + ex.Message);
                return StatusCode(500, "Internal server error");
            }
        }


        // POST: api/Reservations
        [HttpPost]
        public async Task<ActionResult<Reservations>> PostReservation([FromBody] Reservations reservation)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(reservation.FullName) ||
                    string.IsNullOrWhiteSpace(reservation.PaymentMethod) ||
                    reservation.Room_id <= 0)
                {
                    Console.WriteLine("Reservation is missing required fields.");
                    return BadRequest("Required fields are missing.");
                }

                if (reservation.From_date >= reservation.To_date)
                {
                    Console.WriteLine("From_date must be before To_date.");
                    return BadRequest("Invalid date range.");
                }

                var room = await _context.Rooms.FindAsync(reservation.Room_id);
                if (room == null)
                {
                    Console.WriteLine("Room not found with ID: " + reservation.Room_id);
                    return NotFound("Room not found.");
                }

                var overlapping = await _context.Reservations
                    .Where(r => r.Room_id == reservation.Room_id &&
                                r.From_date < reservation.To_date &&
                                r.To_date > reservation.From_date)
                    .FirstOrDefaultAsync();

                if (overlapping != null)
                {
                    Console.WriteLine("Overlapping reservation found for Room ID " + reservation.Room_id);
                    return BadRequest("Room is already reserved for the selected date range.");
                }

                room.Booked = 1;
                _context.Rooms.Update(room);

                _context.Reservations.Add(reservation);
                await _context.SaveChangesAsync();

                Console.WriteLine("Reservation created successfully. ID: " + reservation.Reservation_id);
                return CreatedAtAction("GetReservation", new { id = reservation.Reservation_id }, reservation);
            }
            catch (Exception ex)
            {
                var innerMessage = ex.InnerException != null ? ex.InnerException.Message : "No inner exception";
                Console.WriteLine($"Reservation error: {ex.Message} - Inner exception: {innerMessage}");
                return StatusCode(500, $"Internal Server Error: {ex.Message} - Inner exception: {innerMessage}");
            }
        }

        // PUT: api/Reservations/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutReservation(int id, [FromBody] Reservations updatedReservation)
        {
            if (id != updatedReservation.Reservation_id)
            {
                return BadRequest("ID in URL does not match the reservation ID in body.");
            }

            // 1. Find the existing reservation
            var existingReservation = await _context.Reservations.FindAsync(id);
            if (existingReservation == null)
            {
                return NotFound("Reservation not found.");
            }

            // 2. Optionally release the old room if room ID has changed
            if (existingReservation.Room_id != updatedReservation.Room_id)
            {
                var oldRoom = await _context.Rooms.FindAsync(existingReservation.Room_id);
                if (oldRoom != null)
                {
                    oldRoom.Booked = 0;
                    _context.Rooms.Update(oldRoom);
                }

                var newRoom = await _context.Rooms.FindAsync(updatedReservation.Room_id);
                if (newRoom == null)
                {
                    return NotFound($"Room with ID {updatedReservation.Room_id} does not exist.");
                }

                newRoom.Booked = 1;
                _context.Rooms.Update(newRoom);
            }

            // 3. Update fields manually
            existingReservation.FullName = updatedReservation.FullName;
            existingReservation.Email = updatedReservation.Email;
            existingReservation.PhoneNumber = updatedReservation.PhoneNumber;
            existingReservation.IdentificationNr = updatedReservation.IdentificationNr;
            existingReservation.PaymentMethod = updatedReservation.PaymentMethod;
            existingReservation.From_date = updatedReservation.From_date;
            existingReservation.To_date = updatedReservation.To_date;
            existingReservation.Room_id = updatedReservation.Room_id;

            // 4. Save changes
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error saving updates: {ex.Message}");
            }

            return Ok("Reservation updated successfully.");
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