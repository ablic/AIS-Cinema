using AIS_Cinema.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AIS_Cinema.Controllers
{
    public class SessionsController : Controller
    {
        public const int NumberAvailableSessionDays = 7;
        private readonly AISCinemaDbContext _context;

        public SessionsController(AISCinemaDbContext context)
        {
            _context = context;
        }

        [HttpGet("sessions/{date?}")]
        public async Task<IActionResult> Index(DateTime? date)
        {
            if (!date.HasValue)
            {
                return RedirectToAction(nameof(Index), new { date = DateTime.Today.ToString("yyyy-MM-dd") });
            }

            List<SessionCard> cards = await _context.Sessions
                .Where(s => s.DateTime.Date == date)
                .Include(s => s.Movie)
                .Include(s => s.Tickets)
                .Select(s => new SessionCard
                {
                    SessionId = s.Id,
                    MovieName = s.Movie.Name,
                    DateTimeStr = s.DateTime.ToString("HH:mm"),
                    MovieGenreNames = s.Movie.Genres.Select(g => g.Name).ToList(),
                    Price = s.MinPrice,
                    NumberAvailableSeats = s.Tickets.Where(t => t.OwnerEmail == null).Count(),
                    HallNumber = s.HallId,
                })
                .ToListAsync();

            return View(new SessionsWithDates
            {
                DateTabs = DateTimeUtility.BuildSessionDateTabs((DateTime)date),
                SessionCards = cards
            });
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var session = await _context.Sessions
                .Include(s => s.Hall)
                .Include(s => s.Movie)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (session == null)
            {
                return NotFound();
            }

            return View(session);
        }
    }
}
