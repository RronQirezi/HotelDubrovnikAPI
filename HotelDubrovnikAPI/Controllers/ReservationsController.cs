using Microsoft.AspNetCore.Mvc;
using HotelDubrovnikAPI.Models;
//using Microsoft.AspNetCore.Authorization;

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

        // GET: api/Reservations/5
        //[Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult<Reservations>> GetReservation(int id) // Gets a reservation by ID
        {
            var reservation = await _context.Reservations
                .FirstOrDefaultAsync(r => r.Reservation_id == id);

            if (reservation == null)
            {
                return NotFound();
            }

            return reservation;
        }

        // GET: api/Reservations
        //[Authorize]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Reservations>>> GetReservations() // Gets all reservations, token required
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
        //[AllowAnonymous]
        [HttpPost]
        public async Task<ActionResult<Reservations>> PostReservation([FromBody] Reservations reservation) // Creates a reservation, no token required
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
        //[Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutReservation(int id, [FromBody] Reservations updatedReservation)
        {
            if (id != updatedReservation.Reservation_id)//Check if current reservation ID matches the updated reservation ID
            {
                return BadRequest("ID in URL does not match the reservation ID in body.");
            }

            var existingReservation = await _context.Reservations.FindAsync(id);//Check for existing reservation by ID.
            if (existingReservation == null)//Check if reservation exists
            {
                return NotFound("Reservation not found.");
            }

            if (existingReservation.Room_id != updatedReservation.Room_id)//Checks if existing reservation matches updated reservation in relation to a room id.
            {
                var oldRoom = await _context.Rooms.FindAsync(existingReservation.Room_id);
                if (oldRoom != null)//Checks if old room is reserved.
                {
                    oldRoom.Booked = 0;
                    _context.Rooms.Update(oldRoom);//changes the room status to not booked.
                }

                var newRoom = await _context.Rooms.FindAsync(updatedReservation.Room_id);//new room reservation
                if (newRoom == null)
                {
                    return NotFound($"Room with ID {updatedReservation.Room_id} does not exist.");
                }

                newRoom.Booked = 1;
                _context.Rooms.Update(newRoom);
            }
            //Fields to be updated to the reservation
            existingReservation.FullName = updatedReservation.FullName;
            existingReservation.Email = updatedReservation.Email;
            existingReservation.PhoneNumber = updatedReservation.PhoneNumber;
            existingReservation.IdentificationNr = updatedReservation.IdentificationNr;
            existingReservation.PaymentMethod = updatedReservation.PaymentMethod;
            existingReservation.From_date = updatedReservation.From_date;
            existingReservation.To_date = updatedReservation.To_date;
            existingReservation.Room_id = updatedReservation.Room_id;

            try
            {
                await _context.SaveChangesAsync();//Reservation is updated
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error saving updates: {ex.Message}");
            }

            return Ok("Reservation updated successfully.");
        }

        // DELETE: api/Reservations/5
        //[Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReservation(int id) 
        {
            var reservation = await _context.Reservations.FindAsync(id);//Finds reservation by ID
            if (reservation == null)
            {
                return NotFound();
            }

            var room = await _context.Rooms.FindAsync(reservation.Room_id);//Finds the room by ID that is bound to said reservation
            if (room != null)
            {
                room.Booked = 0; // Mark room available again
                _context.Rooms.Update(room);
            }

            _context.Reservations.Remove(reservation);//Reservation is deleted from the database
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
