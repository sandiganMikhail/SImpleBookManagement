using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SImpleBookManagement.Data;
using SImpleBookManagement.Models;

namespace SImpleBookManagement.Controllers
{
    [Authorize]
    public class PatronsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PatronsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Patrons
        public async Task<IActionResult> Index()
        {
              return _context.Patron != null ? 
                          View(await _context.Patron.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.Patron'  is null.");
        }

        // GET: Patrons/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Patron == null)
            {
                return NotFound();
            }

            var patron = await _context.Patron
                .FirstOrDefaultAsync(m => m.Id == id);
            if (patron == null)
            {
                return NotFound();
            }

            return View(patron);
        }

        // GET: Patrons/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Patrons/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Email")] Patron patron)
        {
            if (ModelState.IsValid)
            {
                _context.Add(patron);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(patron);
        }

        // GET: Patrons/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Patron == null)
            {
                return NotFound();
            }

            var patron = await _context.Patron.FindAsync(id);
            if (patron == null)
            {
                return NotFound();
            }
            return View(patron);
        }

        // POST: Patrons/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Email")] Patron patron)
        {
            if (id != patron.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(patron);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PatronExists(patron.Id))
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
            return View(patron);
        }

        // GET: Patrons/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Patron == null)
            {
                return NotFound();
            }

            var patron = await _context.Patron
                .FirstOrDefaultAsync(m => m.Id == id);
            if (patron == null)
            {
                return NotFound();
            }

            return View(patron);
        }

        // POST: Patrons/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Patron == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Patron'  is null.");
            }
            var patron = await _context.Patron.FindAsync(id);
            if (patron != null)
            {
                _context.Patron.Remove(patron);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PatronExists(int id)
        {
          return (_context.Patron?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
