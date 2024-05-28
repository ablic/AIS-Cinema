using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AIS_Cinema;
using AIS_Cinema.Models;

namespace AIS_Cinema.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AgeLimitsController : Controller
    {
        private readonly AISCinemaDbContext _context;

        public AgeLimitsController(AISCinemaDbContext context)
        {
            _context = context;
        }

        // GET: Admin/AgeLimits
        public async Task<IActionResult> Index()
        {
            return View(await _context.AgeLimits.ToListAsync());
        }

        // GET: Admin/AgeLimits/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ageLimit = await _context.AgeLimits
                .FirstOrDefaultAsync(m => m.Id == id);
            if (ageLimit == null)
            {
                return NotFound();
            }

            return View(ageLimit);
        }

        // GET: Admin/AgeLimits/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/AgeLimits/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Value")] AgeLimit ageLimit)
        {
            if (ModelState.IsValid)
            {
                _context.Add(ageLimit);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(ageLimit);
        }

        // GET: Admin/AgeLimits/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ageLimit = await _context.AgeLimits.FindAsync(id);
            if (ageLimit == null)
            {
                return NotFound();
            }
            return View(ageLimit);
        }

        // POST: Admin/AgeLimits/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Value")] AgeLimit ageLimit)
        {
            if (id != ageLimit.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(ageLimit);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AgeLimitExists(ageLimit.Id))
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
            return View(ageLimit);
        }

        // GET: Admin/AgeLimits/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ageLimit = await _context.AgeLimits
                .FirstOrDefaultAsync(m => m.Id == id);
            if (ageLimit == null)
            {
                return NotFound();
            }

            return View(ageLimit);
        }

        // POST: Admin/AgeLimits/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var ageLimit = await _context.AgeLimits.FindAsync(id);
            if (ageLimit != null)
            {
                _context.AgeLimits.Remove(ageLimit);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AgeLimitExists(int id)
        {
            return _context.AgeLimits.Any(e => e.Id == id);
        }
    }
}
