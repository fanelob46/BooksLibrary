using BooksManagement.Models;

namespace BooksManagement.ViewModels
{
    public class ShoppingCartViewModel
    {
        public List<ShoppingCartItems> CartItems { get; set; }
        public decimal? TotalPrice { get; set; }
        public int? TotalQuantity { get; set; }

    }
}
