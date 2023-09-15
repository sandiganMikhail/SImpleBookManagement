using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SImpleBookManagement.Models;

namespace SImpleBookManagement.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<SImpleBookManagement.Models.Book>? Book { get; set; }
        public DbSet<SImpleBookManagement.Models.BookReservation>? BookReservation { get; set; }
        public DbSet<SImpleBookManagement.Models.Patron>? Patron { get; set; }
        public DbSet<SImpleBookManagement.Models.BorrowedBook>? BorrowedBook { get; set; }
        public DbSet<SImpleBookManagement.Models.BookReview>? BookReview { get; set; }
    }
}