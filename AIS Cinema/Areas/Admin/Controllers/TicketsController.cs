using AIS_Cinema.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace AIS_Cinema.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class TicketsController : Controller
    {
        private readonly AISCinemaDbContext _context;

        public TicketsController(AISCinemaDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index([FromQuery] int? sessionId)
        {
            if (sessionId == null)
            {
                return View(await _context.Tickets
                    .Include(t => t.Session)
                    .ToListAsync());
            }

            return View(await _context.Tickets
                .Where(t => t.SessionId == sessionId)
                .Include(t => t.Session)
                .ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ticket = await _context.Tickets
                .Include(t => t.Session)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (ticket == null)
            {
                return NotFound();
            }

            return View(ticket);
        }

        public IActionResult Create()
        {
            ViewData["SessionId"] = new SelectList(_context.Sessions, "Id", "Id");
            ViewData["VisitorId"] = new SelectList(_context.Users, "Id", "Id");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,SessionId,RowNumber,SeatNumber,Price,OwnerEmail")] Ticket ticket)
        {
            if (ModelState.IsValid)
            {
                _context.Add(ticket);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewData["SessionId"] = new SelectList(_context.Sessions, "Id", "Id", ticket.SessionId);
            return View(ticket);
        }

        /*public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ticket = await _context.Tickets.FindAsync(id);
            if (ticket == null)
            {
                return NotFound();
            }

            ViewData["SessionId"] = new SelectList(_context.Sessions, "Id", "Id", ticket.SessionId);
            return View(ticket);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,SessionId,RowNumber,SeatNumber,Price,OwnerEmail")] Ticket ticket)
        {
            if (id != ticket.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(ticket);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TicketExists(ticket.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }

            ViewData["SessionId"] = new SelectList(_context.Sessions, "Id", "Id", ticket.SessionId);
            return View(ticket);
        }*/

        // GET: Admin/Tickets/Delete/5
        /*public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ticket = await _context.Tickets
                .Include(t => t.Session)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (ticket == null)
            {
                return NotFound();
            }

            return View(ticket);
        }

        // POST: Admin/Tickets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var ticket = await _context.Tickets.FindAsync(id);
            if (ticket != null)
            {
                _context.Tickets.Remove(ticket);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }*/

        private bool TicketExists(int id)
        {
            return _context.Tickets.Any(e => e.Id == id);
        }
    }
}
