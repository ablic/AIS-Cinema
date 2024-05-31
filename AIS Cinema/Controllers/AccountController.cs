using AIS_Cinema.Models;
using AIS_Cinema.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AIS_Cinema.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private readonly AISCinemaDbContext _context;
        private readonly UserManager<Visitor> _userManager;

        public AccountController(AISCinemaDbContext context, UserManager<Visitor> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<IActionResult> GetTickets()
        {
            var user = await _userManager.GetUserAsync(User);
            var email = await _userManager.GetEmailAsync(user);
            var tickets = await _context.Tickets
                .Include(t => t.Session)
                .ThenInclude(s => s.Movie)
                .Where(t => t.OwnerEmail == email)
                .Select(t => new UserTicket
                {
                    SessionDateTimeStr = DateTimeFormatter.FormatDateTime(t.Session.DateTime),
                    MovieName = t.Session.Movie.Name,
                    RowAndSeatStr = TicketFormatter.FormatTicket(t),
                    Price = t.Price,
                    QrCode = t.GetQrCode(),
                })
                .ToListAsync();

            return View(tickets);
        }
    }
}
