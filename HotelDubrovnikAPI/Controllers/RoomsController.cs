using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using HotelDubrovnikAPI.Models;

namespace HotelDubrovnikAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoomsController : ControllerBase
    {
        private readonly DataContext _context;

        public RoomsController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Rooms>>> Get()
        {
            return Ok(await _context.Rooms.ToListAsync());
        }
    }
}
