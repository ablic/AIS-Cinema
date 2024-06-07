using AIS_Cinema.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AIS_Cinema.Controllers.API
{
    [ApiController]
    [Route("api/[controller]")]
    public class SessionsController : ControllerBase
    {
        private readonly AISCinemaDbContext _context;

        public SessionsController(AISCinemaDbContext context)
        {
            _context = context;
        }

        [HttpGet("{date}")]
        public async Task<ActionResult<IEnumerable<Session>>> GetSessions(DateTime date)
        {
            var sessions = await _context.Sessions
                .Where(s => s.DateTime.Date == date)
                .Include(s => s.Movie)
                .ToListAsync();

            return Ok(sessions);
        }
    }
}
