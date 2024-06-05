using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AIS_Cinema;
using AIS_Cinema.Models;
using AIS_Cinema.ViewModels;

namespace AIS_Cinema.Controllers
{
    public class MoviesController : Controller
    {
        private readonly AISCinemaDbContext _context;

        public MoviesController(AISCinemaDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            List<MovieCard> cards = await _context.Movies
                .Select(m => new MovieCard { MovieId = m.Id, Title = m.Name, PosterPath = m.PosterPath ?? string.Empty })
                .ToListAsync();

            return View(cards);
        }

        public async Task<IActionResult> Details(int? id, [FromQuery] DateTime? date)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movie = await _context.Movies
                .Include(m => m.AgeLimit)
                .Include(m => m.Genres)
                .Include(m => m.Countries)
                .Include(m => m.Sessions)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (movie == null)
            {
                return NotFound();
            }

            date ??= DateTime.Today;

            var sessionCards = await _context.Sessions
                .Where(s => s.MovieId == id && s.DateTime.Date == date)
                .Select(s => new MovieSessionCard
                {
                    SessionId = s.Id,
                    TimeStr = s.DateTime.ToString("HH:mm"),
                    NumberAvailableSeats = s.Tickets.Count,
                    Price = s.MinPrice,
                    HallNumber = s.HallId,
                })
                .ToListAsync();

            return View(new MovieDetails
            {
                Movie = movie,
                DateTabs = DateTimeUtility.BuildSessionDateTabs((DateTime)date),
                SessionCards = sessionCards
            });
        }
    }
}