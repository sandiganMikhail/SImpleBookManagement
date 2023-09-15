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
    public class BookReservationsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BookReservationsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: BookReservations
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.BookReservation.Include(b => b.Book).Include(b => b.Patron);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: BookReservations/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.BookReservation == null)
            {
                return NotFound();
            }

            var bookReservation = await _context.BookReservation
                .Include(b => b.Book)
                .Include(b => b.Patron)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (bookReservation == null)
            {
                return NotFound();
            }

            return View(bookReservation);
        }

        // GET: BookReservations/Create
        public IActionResult Create()
        {
            ViewData["BookId"] = new SelectList(_context.Book, "Id", "Title");
            ViewData["PatronId"] = new SelectList(_context.Set<Patron>(), "Id", "Name");
            return View();
        }

        // POST: BookReservations/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,PatronId,BookId,ReservationDate")] BookReservation bookReservation)
        {
            if (ModelState.IsValid)
            {
                _context.Add(bookReservation);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["BookId"] = new SelectList(_context.Book, "Id", "Id", bookReservation.BookId);
            ViewData["PatronId"] = new SelectList(_context.Set<Patron>(), "Id", "Id", bookReservation.PatronId);
            return View(bookReservation);
        }

        // GET: BookReservations/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.BookReservation == null)
            {
                return NotFound();
            }

            var bookReservation = await _context.BookReservation.FindAsync(id);
            if (bookReservation == null)
            {
                return NotFound();
            }
            ViewData["BookId"] = new SelectList(_context.Book, "Id", "Id", bookReservation.BookId);
            ViewData["PatronId"] = new SelectList(_context.Set<Patron>(), "Id", "Id", bookReservation.PatronId);
            return View(bookReservation);
        }

        // POST: BookReservations/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,PatronId,BookId,ReservationDate")] BookReservation bookReservation)
        {
            if (id != bookReservation.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(bookReservation);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BookReservationExists(bookReservation.Id))
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
            ViewData["BookId"] = new SelectList(_context.Book, "Id", "Id", bookReservation.BookId);
            ViewData["PatronId"] = new SelectList(_context.Set<Patron>(), "Id", "Id", bookReservation.PatronId);
            return View(bookReservation);
        }

        // GET: BookReservations/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.BookReservation == null)
            {
                return NotFound();
            }

            var bookReservation = await _context.BookReservation
                .Include(b => b.Book)
                .Include(b => b.Patron)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (bookReservation == null)
            {
                return NotFound();
            }

            return View(bookReservation);
        }

        // POST: BookReservations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.BookReservation == null)
            {
                return Problem("Entity set 'ApplicationDbContext.BookReservation'  is null.");
            }
            var bookReservation = await _context.BookReservation.FindAsync(id);
            if (bookReservation != null)
            {
                _context.BookReservation.Remove(bookReservation);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BookReservationExists(int id)
        {
          return (_context.BookReservation?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
