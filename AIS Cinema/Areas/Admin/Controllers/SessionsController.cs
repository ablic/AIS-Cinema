using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AIS_Cinema;
using AIS_Cinema.Models;
using AIS_Cinema.Areas.Admin.Models;
using AIS_Cinema.Areas.Admin.ViewModels;
using Newtonsoft.Json;
using AIS_Cinema.Models.HallLayout;

namespace AIS_Cinema.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SessionsController : Controller
    {
        private readonly AISCinemaDbContext _context;

        public SessionsController(AISCinemaDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var aISCinemaDbContext = _context.Sessions.Include(s => s.Hall).Include(s => s.Movie);
            return View(await aISCinemaDbContext.ToListAsync());
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

        public IActionResult Create()
        {
            ViewData["HallId"] = new SelectList(_context.Halls, "Id", "Id");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Id,DateTime,HallId,MinPrice")] SessionPrimaryData sessionPrimaryData)
        {
            if (ModelState.IsValid)
            {
                TempData["SessionPrimaryData"] = JsonConvert.SerializeObject(sessionPrimaryData);
                return RedirectToAction(nameof(BindMovie));
            }

            ViewData["HallId"] = new SelectList(_context.Set<Hall>(), "Id", "Id", sessionPrimaryData.HallId);
            return View(sessionPrimaryData);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var session = await _context.Sessions.FindAsync(id);
            if (session == null)
            {
                return NotFound();
            }

            ViewData["HallId"] = new SelectList(_context.Set<Hall>(), "Id", "Id", session.HallId);
            return View(new SessionPrimaryData
            {
                Id = session.Id,
                DateTime = session.DateTime,
                HallId = session.HallId,
                MinPrice = session.MinPrice,
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("Id,DateTime,HallId,MinPrice")] SessionPrimaryData sessionPrimaryData)
        {
            if (id != sessionPrimaryData.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                TempData["SessionPrimaryData"] = JsonConvert.SerializeObject(sessionPrimaryData);
                return RedirectToAction(nameof(BindMovie));
            }

            ViewData["HallId"] = new SelectList(_context.Set<Hall>(), "Id", "Id", sessionPrimaryData.HallId);
            return View(sessionPrimaryData);
        }

        public async Task<IActionResult> BindMovie()
        {
            SessionPrimaryData? sessionPrimaryData = JsonConvert
                .DeserializeObject<SessionPrimaryData>(TempData.Peek("SessionPrimaryData") as string);

            if (sessionPrimaryData == null)
            {
                return RedirectToAction(nameof(Create));
            }

            List<ModelIdWithTitle> movieIdsAndNames = await _context.Movies
                .Select(m => new ModelIdWithTitle { Id = m.Id, Title = m.Name })
                .ToListAsync();

            return View(new SessionMovieBinding
            {
                DateTimeStr = sessionPrimaryData.DateTime.ToString("dd.MM HH:mm"),
                Movies = movieIdsAndNames,
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> BindMovie(int? selectedMovieId)
        {
            SessionPrimaryData? sessionPrimaryData = JsonConvert
                .DeserializeObject<SessionPrimaryData>(TempData.Peek("SessionPrimaryData") as string);

            if (sessionPrimaryData == null)
            {
                return RedirectToAction(nameof(Create));
            }

            if (selectedMovieId == null)
            {
                List<ModelIdWithTitle> movieIdsAndNames = await _context.Movies
                    .Select(m => new ModelIdWithTitle { Id = m.Id, Title = m.Name })
                    .ToListAsync();

                return View(new SessionMovieBinding
                {
                    DateTimeStr = sessionPrimaryData.DateTime.ToString("dd.MM HH:mm"),
                    Movies = movieIdsAndNames,
                });
            }

            Session session = new Session
            {
                MovieId = (int)selectedMovieId,
                DateTime = sessionPrimaryData.DateTime,
                HallId = sessionPrimaryData.HallId,
                MinPrice = sessionPrimaryData.MinPrice,
            };

            if (sessionPrimaryData.Id != null)
            {
                session.Id = (int)sessionPrimaryData.Id;
                try
                {
                    _context.Update(session);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SessionExists(session.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
            }
            else
            {
                _context.Add(session);
                await _context.SaveChangesAsync();
                await CreateTicketsForSessionAsync(session);
                await _context.SaveChangesAsync();
            }
            
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int? id)
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

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var session = await _context.Sessions.FindAsync(id);
            if (session != null)
            {
                _context.Sessions.Remove(session);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SessionExists(int id)
        {
            return _context.Sessions.Any(e => e.Id == id);
        }

        private async Task CreateTicketsForSessionAsync(Session session)
        {
            Hall hall = await _context.FindAsync<Hall>(session.HallId);
            List<Row> hallLayout = JsonConvert.DeserializeObject<List<Row>>(hall.Schema);

            foreach (var row in hallLayout)
            {
                foreach (var seat in row.Seats)
                {
                    _context.Add(new Ticket
                    {
                        SessionId = session.Id,
                        RowNumber = row.Number,
                        SeatNumber = seat.Number,
                        Price = (decimal)seat.PriceMultiplier * session.MinPrice,
                    });
                }
            }
        }
    }
}
