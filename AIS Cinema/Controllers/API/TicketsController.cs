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

        [HttpGet("{chatId}")]
        public async Task<ActionResult<IEnumerable<Ticket>>> GetAll (long chatId)
        {
            var user = await _userManager.Users
                .FirstOrDefaultAsync(u => u.TelegramChatId == chatId);

            var tickets = await _context.Tickets
                .Where(t => t.OwnerEmail == user.Email)
                .Include(t => t.Session)
                .ThenInclude(s => s.Movie)
                .ToListAsync();

            return Ok(tickets);
        }

        [HttpGet("{chatId}/{id}")]
        public async Task<ActionResult<Ticket>> GetById(long chatId, int id)
        {
            var user = await _userManager.Users
                .FirstOrDefaultAsync(u => u.TelegramChatId == chatId);

            var ticket = await _context.Tickets
                .Include(t => t.Session)
                .ThenInclude(s => s.Movie)
                .FirstOrDefaultAsync(t => t.Id == id);

            return Ok(ticket);
        }
    }
}
