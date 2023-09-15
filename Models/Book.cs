using System.ComponentModel;

namespace SImpleBookManagement.Models
{
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string? ISBN { get; set; }
        // change display name of TotalCopies to "Total Copies"
        [DisplayName("Total Copies")]
        public int TotalCopies { get; set; } // Total number of copies available
        [DisplayName("Available Copies")]
        public int AvailableCopies { get; set; } // Number of copies available for borrowing
        [DisplayName("Publish On")]
        public DateTime PublicationDate { get; set; }
        public string? Genre { get; set; }
        public string? Description { get; set; }

        // Navigation property for book reservations
        public virtual ICollection<BookReservation>? Reservations { get; set; }

        // Navigation property for book reviews
        public virtual ICollection<BookReview>? Reviews { get; set; }
    }
}
