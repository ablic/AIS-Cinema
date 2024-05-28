using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AIS_Cinema.Models;
using AIS_Cinema.Areas.Admin.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AIS_Cinema.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class MoviesController : Controller
    {
        private readonly AISCinemaDbContext _context;
        private readonly ImageWorker _imageWorker;

        public MoviesController(AISCinemaDbContext context, ImageWorker imageWorker)
        {
            _context = context;
            _imageWorker = imageWorker;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.Movies.ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movie = await _context.Movies
                .FirstOrDefaultAsync(m => m.Id == id);
            if (movie == null)
            {
                return NotFound();
            }

            return View(movie);
        }

        public IActionResult Create()
        {
            ViewData["AgeLimitId"] = new SelectList(_context.AgeLimits, "Id", "Value");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Duration,ProductionYear,ReleaseDate,Description,PosterPath")] Movie movie, IFormFile posterFile)
        {
            if (ModelState.IsValid)
            {
                await TrySavePosterForMovieAsync(movie, posterFile);
                _context.Add(movie);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AgeLimitId"] = new SelectList(_context.AgeLimits, "Id", "Value");
            return View(movie);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movie = await _context.Movies.FindAsync(id);

            if (movie == null)
            {
                return NotFound();
            }

            ViewData["AgeLimitId"] = new SelectList(_context.AgeLimits, "Id", "Value");
            return View(movie);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Duration,ProductionYear,ReleaseDate,Description,PosterPath")] Movie movie, IFormFile posterFile)
        {
            if (id != movie.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await TrySavePosterForMovieAsync(movie, posterFile);
                    _context.Update(movie);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MovieExists(movie.Id))
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

            ViewData["AgeLimitId"] = new SelectList(_context.AgeLimits, "Id", "Value");
            return View(movie);
        }

        public async Task<IActionResult> BindGenres(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Movie? movie = await _context.Movies
                .Include(m => m.Genres)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (movie == null)
            {
                return NotFound();
            }

            var allGenres = await _context.Genres.ToListAsync();
            var viewModel = new MovieGenres
            {
                MovieId = movie.Id,
                MovieName = movie.Name,
                AllGenres = allGenres,
                SelectedGenreIds = movie.Genres.Select(g => g.Id).ToList()
            };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> BindGenres(MovieGenres movieGenres)
        {
            var movie = await _context.Movies
                .Include(m => m.Genres)
                .FirstOrDefaultAsync(m => m.Id == movieGenres.MovieId);

            if (movie == null)
            {
                return NotFound();
            }

            movie.Genres = await _context.Genres
                .Where(g => movieGenres.SelectedGenreIds.Contains(g.Id))
                .ToListAsync();

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> BindCountries(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Movie? movie = await _context.Movies
                .Include(m => m.Countries)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (movie == null)
            {
                return NotFound();
            }

            var allCountries = await _context.Countries.ToListAsync();
            var viewModel = new MovieCountries
            {
                MovieId = movie.Id,
                MovieName = movie.Name,
                AllCountries = allCountries,
                SelectedCountryIds = movie.Countries.Select(c => c.Id).ToList()
            };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> BindCountries(MovieCountries movieCountries)
        {
            var movie = await _context.Movies
                .Include(m => m.Countries)
                .FirstOrDefaultAsync(m => m.Id == movieCountries.MovieId);

            if (movie == null)
            {
                return NotFound();
            }

            movie.Countries = await _context.Countries
                .Where(c => movieCountries.SelectedCountryIds.Contains(c.Id))
                .ToListAsync();

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movie = await _context.Movies
                .FirstOrDefaultAsync(m => m.Id == id);
            if (movie == null)
            {
                return NotFound();
            }

            return View(movie);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var movie = await _context.Movies.FindAsync(id);
            if (movie != null)
            {
                _context.Movies.Remove(movie);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MovieExists(int id)
        {
            return _context.Movies.Any(e => e.Id == id);
        }

        private async Task TrySavePosterForMovieAsync(Movie movie, IFormFile posterFile)
        {
            if (posterFile != null && posterFile.Length > 0)
            {
                if (!string.IsNullOrEmpty(movie.PosterPath))
                {
                    _imageWorker.DeleteImage(movie.PosterPath);
                }
                movie.PosterPath = await _imageWorker.SaveImageAsync(posterFile);
            }
        }
    }
}
