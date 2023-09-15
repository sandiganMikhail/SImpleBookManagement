namespace SImpleBookManagement.Models
{
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string? ISBN { get; set; }
        public int TotalCopies { get; set; } // Total number of copies available
        public int AvailableCopies { get; set; } // Number of copies available for borrowing
        public DateTime PublicationDate { get; set; }
        public string? Genre { get; set; }
        public string? Description { get; set; }

        // Navigation property for book reservations
        public virtual ICollection<BookReservation>? Reservations { get; set; }

        // Navigation property for book reviews
        public virtual ICollection<BookReview>? Reviews { get; set; }
    }
}
