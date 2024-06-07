using AIS_Cinema.Models;
using AIS_Cinema.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AIS_Cinema.Controllers.API
{
    [ApiController]
    [Route("api/[controller]")]
    public class TicketsController : ControllerBase
    {
        private AISCinemaDbContext _context;
        private UserManager<Visitor> _userManager;

        public TicketsController(AISCinemaDbContext context, UserManager<Visitor> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [HttpGet("{userId}")]
        public async Task<ActionResult<IEnumerable<Ticket>>> GetAll (string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            var tickets = await _context.Tickets
                .Where(t => t.OwnerEmail == user.Email)
                .Include(t => t.Session)
                .ThenInclude(s => s.Movie)
                .ToListAsync();

            return Ok(tickets);
        }

        [HttpGet("{userId}/{ticketId}")]
        public async Task<ActionResult<Ticket>> GetById(string userId, int ticketId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            var ticket = await _context.Tickets
                .Include(t => t.Session)
                .ThenInclude(s => s.Movie)
                .FirstOrDefaultAsync(t => t.Id == ticketId);

            return Ok(ticket);
        }
    }
}
