namespace SImpleBookManagement.Models
{
    public class Patron
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }

        // Navigation property for patron's borrowed books
        public virtual ICollection<BorrowedBook>? BorrowedBooks { get; set; }

        // Navigation property for patron's reservations
        public virtual ICollection<BookReservation>? Reservations { get; set; }
    }
}
