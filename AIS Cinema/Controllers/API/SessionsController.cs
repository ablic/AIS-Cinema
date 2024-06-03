using API_Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AIS_Cinema.Controllers.API
{
    [ApiController]
    public class SessionsController : ControllerBase
    {
        private readonly AISCinemaDbContext _context;

        public SessionsController(AISCinemaDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("api/sessions")]
        public async Task<ActionResult<IEnumerable<Session>>> GetSessions()
        {
            var sessions = await _context.Sessions
                .Include(s => s.Movie)
                .Select(s => new Session
                {
                    DateTime = s.DateTime,
                    MovieName = s.Movie.Name
                })
                .ToListAsync();

            return Ok(sessions);
        }
    }
}
