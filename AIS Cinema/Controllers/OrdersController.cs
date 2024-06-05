using AIS_Cinema.Models.HallLayout;
using AIS_Cinema.Models;
using AIS_Cinema.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using AIS_Cinema.Services;

namespace AIS_Cinema.Controllers
{
    public class OrdersController : Controller
    {
        private readonly AISCinemaDbContext _context;
        private readonly UserManager<Visitor> _userManager;
        private readonly TicketEmailSender _ticketEmailSender;

        public OrdersController(AISCinemaDbContext context, UserManager<Visitor> userManager, TicketEmailSender ticketEmailSender)
        {
            _context = context;
            _userManager = userManager;
            _ticketEmailSender = ticketEmailSender;
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

            TempData["OrderSessionId"] = session.Id;
            TempData["OrderDateTime"] = session.DateTime;
            TempData["OrderMovieName"] = session.Movie.Name;

            return View(CollectSeatSelectionData(session));
        }

        [HttpPost]
        public async Task<IActionResult> SelectSeats(List<int> selectedTicketIds)
        {
            if (selectedTicketIds == null || selectedTicketIds.Count == 0)
            {
                int sessionId = (int)TempData.Peek("OrderSessionId");

                var session = await _context.Sessions
                    .Include(s => s.Hall)
                    .Include(s => s.Movie)
                    .Include(s => s.Tickets)
                    .FirstOrDefaultAsync(m => m.Id == sessionId);

                if (session == null)
                {
                    return NotFound();
                }

                return View(CollectSeatSelectionData(session));
            }

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
            List<Ticket> tickets = await GetOrderTicketsAsync(true);

            return View(new OrderConfirmation
            {
                DateTimeStr = DateTimeUtility.FormatDateTime((DateTime)TempData["OrderDateTime"]),
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
        public IActionResult Pay(Payment payment)
        {
            if (!ModelState.IsValid)
            {
                return View(payment);
            }

            return RedirectToAction(nameof(PaymentSuccess));
        }

        public async Task<IActionResult> PaymentSuccess()
        {
            int sessionId = (int)TempData.Peek("OrderSessionId");
            var session = await _context.Sessions
                .Include(s => s.Hall)
                .Include(s => s.Movie)
                .FirstOrDefaultAsync(m => m.Id == sessionId);

            if (session == null)
            {
                return NotFound();
            }

            string email = TempData["OrderEmail"].ToString();
            List<Ticket> tickets = await GetOrderTicketsAsync(false);

            tickets.ForEach(t => t.OwnerEmail = email);
            _context.UpdateRange(tickets);
            await _context.SaveChangesAsync();

            await _ticketEmailSender.SendTicketsAsync(email, session, tickets);

            return View();
        }

        private SeatSelection CollectSeatSelectionData(Session session)
        {
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

            return seatSelection;
        }

        private async Task<List<Ticket>> GetOrderTicketsAsync(bool keepTempData)
        {
            List<int> ticketIds = JsonConvert.DeserializeObject<List<int>>(
                TempData["OrderTickets"].ToString());

            if (keepTempData)
            {
                TempData.Keep();
            }

            return await _context.Tickets
                .Where(t => ticketIds.Contains(t.Id))
                .ToListAsync();
        }
    }
}
