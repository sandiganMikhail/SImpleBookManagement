namespace SImpleBookManagement.Models
{
    public class BorrowedBook
    {
        public int Id { get; set; }
        public int PatronId { get; set; }
        public int BookId { get; set; }
        public DateTime BorrowDate { get; set; }
        public DateTime DueDate { get; set; }

        // Navigation property for the associated patron
        public virtual Patron? Patron { get; set; }

        // Navigation property for the borrowed book
        public virtual Book? Book { get; set; }
    }
}