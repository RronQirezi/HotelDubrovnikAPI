using HotelDubrovnikAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;




namespace HotelDubrovnikAPI.Controllers
{


    [Route("api/[controller]")]
    [ApiController]
    public class EventsController : ControllerBase
    {
        private readonly DataContext _context;

        public EventsController(DataContext context) 
        {
            _context = context;
        }


        [HttpGet]
        public async Task<ActionResult<List<Event>>> Get()
        {
            
            return Ok(await _context.Events.ToListAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<List<Event>>> Get(int id)
        {
            var eventi = await _context.Events.FindAsync(id);

            if (eventi == null)
            {
                return BadRequest("Event not found by ID");
            }
            return Ok(eventi);

        }



        [HttpPost]
        public async Task<ActionResult<List<Event>>> Post([FromQuery]Event eventi)
        {
            _context.Events.Add(eventi);
            await _context.SaveChangesAsync();

            return Ok(await _context.Events.ToListAsync());
        }

        [HttpPut]
        public async Task<ActionResult<List<Event>>> Put(Event eventi)
        {
            var dbHoteli = await _context.Events.FindAsync(eventi.EventId);

            if(dbHoteli == null)
            {
                return BadRequest("No event found");
            }

            dbHoteli.EventTitle = eventi.EventTitle;
            dbHoteli.EventDateFrom = eventi.EventDateFrom;
            dbHoteli.EventDateTo = eventi.EventDateTo;
            dbHoteli.EventImage = eventi.EventImage;
            dbHoteli.EventDescription = eventi.EventDescription;
            dbHoteli.EventURL = eventi.EventURL;

            await _context.SaveChangesAsync();

            return Ok(await _context.Events.ToListAsync());
        }


        [HttpDelete("{EventId}")]
        public async Task<ActionResult<List<Event>>> Delete (int id)
        {
            var dbHoteli = await _context.Events.FindAsync(id);
            if(dbHoteli == null)
            {
                return BadRequest("Event Not Found");
            }

            _context.Events.Remove(dbHoteli);

            await _context.SaveChangesAsync();

            return Ok(await _context.Events.ToListAsync());
        }

        
    }
}
