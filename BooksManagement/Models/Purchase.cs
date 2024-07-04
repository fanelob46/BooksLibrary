namespace BooksManagement.Models
{
    public class Purchase
    {
        public int Id { get; set; }
        public int BookId {  get; set; }
        public Book Book { get; set; }
        public int? Quantinty { get; set; }
        public DateTime? PurchaseDate { get; set; }
        public decimal? Total { get; set;}
    }
}
