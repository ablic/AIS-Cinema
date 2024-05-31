using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using API_Models;

namespace AIS_Cinema.Controllers.API
{
    [ApiController]
    public class ApiController : ControllerBase
    {
        private readonly AISCinemaDbContext _context;

        public ApiController(AISCinemaDbContext context)
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

        [HttpGet]
        [Route("api/hi")]
        public async Task<ActionResult<string>> Hi()
        {
            return Ok("hi");
        }
    }
}
