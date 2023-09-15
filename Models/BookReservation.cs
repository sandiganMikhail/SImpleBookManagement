namespace SImpleBookManagement.Models
{
    public class BookReservation
    {
        public int Id { get; set; }
        public int PatronId { get; set; }
        public int BookId { get; set; }
        public DateTime ReservationDate { get; set; }

        // Navigation property for the associated patron
        public virtual Patron? Patron { get; set; }

        // Navigation property for the reserved book
        public virtual Book? Book { get; set; }

    }
}