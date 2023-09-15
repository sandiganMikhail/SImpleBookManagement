namespace SImpleBookManagement.Models
{
    public class BookReview
    {
        public int Id { get; set; }
        public int PatronId { get; set; }
        public int BookId { get; set; }
        public string ReviewText { get; set; }
        public int Rating { get; set; }
        public DateTime ReviewDate { get; set; }

        // Navigation property for the associated patron
        public virtual Patron? Patron { get; set; }

        // Navigation property for the reviewed book
        public virtual Book? Book { get; set; }
    }
}