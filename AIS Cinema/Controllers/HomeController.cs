using AIS_Cinema.Models;
using AIS_Cinema.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace AIS_Cinema.Controllers
{
    public class HomeController : Controller
    {
        private readonly AISCinemaDbContext _context;
        private readonly ILogger<HomeController> _logger;

        public HomeController(AISCinemaDbContext context, ILogger<HomeController> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            List<MovieCard> cards = await _context.Movies
                .Select(m => new MovieCard { MovieId = m.Id, Title = m.Name, PosterPath = m.PosterPath})
                .ToListAsync();

            return View(cards);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
