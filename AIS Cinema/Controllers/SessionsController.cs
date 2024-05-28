using AIS_Cinema.Models;
using AIS_Cinema.Models.HallLayout;
using AIS_Cinema.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace AIS_Cinema.Controllers
{
    public class SessionsController : Controller
    {
        private const int NumberAvailableSessionDays = 5;
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

            List<SessionDateTab> tabs = new List<SessionDateTab>(NumberAvailableSessionDays);

            for (int i = 0; i < NumberAvailableSessionDays; i++)
            {
                SessionDateTab tab = new SessionDateTab();
                tab.Date = DateTime.Today.AddDays(i);
                tab.Text = tab.DateStr;
                
                if (tab.Date == date)
                {
                    tab.State = SessionDateTab.TabState.Selected;
                }
                else if (await _context.Sessions.Where(s => s.DateTime.Date == tab.Date).CountAsync() == 0)
                {
                    tab.State = SessionDateTab.TabState.Disabled;
                }
                else
                {
                    tab.State = SessionDateTab.TabState.NotSelected;
                }

                tabs.Add(tab);
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
                    NumberAvailableSeats = s.Tickets.Count
                })
                .ToListAsync();

            return View(new SessionsWithDates
            {
                DateTabs = tabs,
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
