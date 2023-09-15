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
    public class BookReviewsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BookReviewsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: BookReviews
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.BookReview.Include(b => b.Book).Include(b => b.Patron);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: BookReviews/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.BookReview == null)
            {
                return NotFound();
            }

            var bookReview = await _context.BookReview
                .Include(b => b.Book)
                .Include(b => b.Patron)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (bookReview == null)
            {
                return NotFound();
            }

            return View(bookReview);
        }

        // GET: BookReviews/Create
        public IActionResult Create()
        {
            ViewData["BookId"] = new SelectList(_context.Book, "Id", "Id");
            ViewData["PatronId"] = new SelectList(_context.Patron, "Id", "Id");
            return View();
        }

        // POST: BookReviews/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,PatronId,BookId,ReviewText,Rating,ReviewDate")] BookReview bookReview)
        {
            if (ModelState.IsValid)
            {
                _context.Add(bookReview);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["BookId"] = new SelectList(_context.Book, "Id", "Id", bookReview.BookId);
            ViewData["PatronId"] = new SelectList(_context.Patron, "Id", "Id", bookReview.PatronId);
            return View(bookReview);
        }

        // GET: BookReviews/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.BookReview == null)
            {
                return NotFound();
            }

            var bookReview = await _context.BookReview.FindAsync(id);
            if (bookReview == null)
            {
                return NotFound();
            }
            ViewData["BookId"] = new SelectList(_context.Book, "Id", "Id", bookReview.BookId);
            ViewData["PatronId"] = new SelectList(_context.Patron, "Id", "Id", bookReview.PatronId);
            return View(bookReview);
        }

        // POST: BookReviews/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,PatronId,BookId,ReviewText,Rating,ReviewDate")] BookReview bookReview)
        {
            if (id != bookReview.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(bookReview);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BookReviewExists(bookReview.Id))
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
            ViewData["BookId"] = new SelectList(_context.Book, "Id", "Id", bookReview.BookId);
            ViewData["PatronId"] = new SelectList(_context.Patron, "Id", "Id", bookReview.PatronId);
            return View(bookReview);
        }

        // GET: BookReviews/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.BookReview == null)
            {
                return NotFound();
            }

            var bookReview = await _context.BookReview
                .Include(b => b.Book)
                .Include(b => b.Patron)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (bookReview == null)
            {
                return NotFound();
            }

            return View(bookReview);
        }

        // POST: BookReviews/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.BookReview == null)
            {
                return Problem("Entity set 'ApplicationDbContext.BookReview'  is null.");
            }
            var bookReview = await _context.BookReview.FindAsync(id);
            if (bookReview != null)
            {
                _context.BookReview.Remove(bookReview);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BookReviewExists(int id)
        {
          return (_context.BookReview?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
