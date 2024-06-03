using AIS_Cinema.Models;
using API_Models;
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
        public async Task<ActionResult<API_Models.Ticket[]>> Get(long chatId)
        {
            var user = await _userManager.Users
                .FirstOrDefaultAsync(u => u.TelegramChatId == chatId);

            var tickets = await _context.Tickets
                .Where(t => t.OwnerEmail == user.Email)
                .Include(t => t.Session)
                .ThenInclude(s => s.Movie)
                .Select(t => new API_Models.Ticket
                {
                    SessionDateTime = t.Session.DateTime,
                    MovieName = t.Session.Movie.Name,
                    RowNumber = t.RowNumber,
                    SeatNumber = t.SeatNumber,
                    QrCode = t.GetQrCode(),
                })
                .ToListAsync();

            return Ok(tickets);
        }
    }
}
