namespace BooksManagement.Models
{
    public class ShoppingCartItems
    {
        public int Id { get; set; }
        public Book Book { get; set; }
        public int Quantity { get; set; }

    }
}
