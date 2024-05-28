using AIS_Cinema.Models.HallLayout;
using AIS_Cinema.Models;
using AIS_Cinema.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace AIS_Cinema.Controllers
{
    public class OrdersController : Controller
    {
        private readonly AISCinemaDbContext _context;
        private readonly UserManager<Visitor> _userManager;

        public OrdersController(AISCinemaDbContext context, UserManager<Visitor> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<IActionResult> SelectSeats(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var session = await _context.Sessions
                .Include(s => s.Hall)
                .Include(s => s.Movie)
                .Include(s => s.Tickets)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (session == null)
            {
                return NotFound();
            }

            TempData["OrderDateTime"] = session.DateTime;
            TempData["OrderMovieName"] = session.Movie.Name;

            List<Row> rows = JsonConvert.DeserializeObject<List<Row>>(session.Hall.Schema);

            SeatSelection seatSelection = new SeatSelection()
            {
                Rows = rows.Select(r => new RowViewModel
                {
                    Seats = r.Seats.Select(s =>
                    {
                        Ticket ticket = session.Tickets
                            .FirstOrDefault(t => t.RowNumber == r.Number && t.SeatNumber == s.Number);

                        return new SeatViewModel
                        {
                            LeftGap = s.LeftGap,
                            RightGap = s.RightGap,
                            Price = (decimal)s.PriceMultiplier * session.MinPrice,
                            TicketId = ticket.Id,
                            IsTaken = ticket.IsBought,
                        };
                    })
                    .ToList(),
                })
                .ToList(),
            };

            return View(seatSelection);
        }

        [HttpPost]
        public IActionResult SelectSeats(List<int> selectedTicketIds)
        {
            TempData["OrderTickets"] = JsonConvert.SerializeObject(selectedTicketIds);
            return RedirectToAction(nameof(EnterEmail));
        }

        public async Task<IActionResult> EnterEmail()
        {
            EmailEntry emailEntry = new EmailEntry();

            if (User.Identity.IsAuthenticated)
            {
                var user = await _userManager.GetUserAsync(User);
                var email = await _userManager.GetEmailAsync(user);

                emailEntry.Email = email;
            }

            return View(emailEntry);
        }

        [HttpPost]
        public async Task<IActionResult> EnterEmail(EmailEntry emailEntry)
        {
            if (!ModelState.IsValid)
            {
                if (User.Identity.IsAuthenticated)
                {
                    var user = await _userManager.GetUserAsync(User);
                    var email = await _userManager.GetEmailAsync(user);

                    emailEntry.Email = email;
                }

                return View(emailEntry);
            }

            TempData["OrderEmail"] = emailEntry.Email;
            return RedirectToAction(nameof(ConfirmOrder));
        }

        public async Task<IActionResult> ConfirmOrder()
        {
            List<int> ticketIds = JsonConvert.DeserializeObject<List<int>>(
                TempData["OrderTickets"].ToString());

            List<Ticket> tickets = await _context.Tickets
                .Where(t => ticketIds.Contains(t.Id))
                .ToListAsync();

            return View(new OrderConfirmation
            {
                DateTimeStr = DateTimeFormatter.FormatDateTime((DateTime)TempData["OrderDateTime"]),
                MovieName = TempData["OrderMovieName"].ToString(),
                Seats = tickets.Select(t => TicketFormatter.FormatTicket(t)).ToList(),
                Email = TempData["OrderEmail"].ToString(),
                Price = tickets.Sum(t => t.Price),
            });
        }

        public IActionResult Pay()
        {
            return View();
        }

        [HttpPost]
        [ActionName("Pay")]
        public IActionResult PayDone()
        {
            return RedirectToAction(nameof(PaymentSuccess));
        }

        public IActionResult PaymentSuccess()
        {
            return View();
        }
    }
}
