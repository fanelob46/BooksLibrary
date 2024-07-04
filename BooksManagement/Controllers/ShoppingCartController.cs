using BooksManagement.Data;
using BooksManagement.Models;
using BooksManagement.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace BooksManagement.Controllers
{
    public class ShoppingCartController : Controller
    {
        public readonly ApplicationDBContext _dbContext;
        public readonly List<ShoppingCartItems> _cartItems;

        public ShoppingCartController(ApplicationDBContext dbContext)
        {
            _dbContext = dbContext;
            _cartItems = new List<ShoppingCartItems>();
        }
        public IActionResult Index()
        {
            return View();
        }
        
        public IActionResult AddToCart(int id)
        {
            var BookToCart = _dbContext.Books.Find(id);

            var cartItems = HttpContext.Session.Get<List<ShoppingCartItems>>("Cart") ?? new List<ShoppingCartItems>();

            var existingCartItem = cartItems.FirstOrDefault(item =>
            item.Book.BookId == id); 

             if (existingCartItem != null)
            {
                existingCartItem.Quantity++;
            }
            else
            {
                cartItems.Add(new ShoppingCartItems
                { 
                    Book = BookToCart, 
                    Quantity = 1 
                });
            }

           HttpContext.Session.Set("Cart", cartItems);

            return RedirectToAction("Index","Home");   
        }
        [HttpGet]
        public IActionResult ViewCart() 
        {

            var cartItems = HttpContext.Session.Get<List<ShoppingCartItems>>("Cart") ?? new List<ShoppingCartItems>();


            var cartViewModel = new ShoppingCartViewModel
            {
                CartItems = cartItems,
                TotalPrice = (decimal?)cartItems.Sum(item =>
                item.Book.Price * item.Quantity)
            };
             
            return View(cartViewModel);
        
        }
        public IActionResult RemoveItem(int id)
        {
            var cartItems = HttpContext.Session.Get<List<ShoppingCartItems>>("Cart") ?? new List<ShoppingCartItems>();
            var itemToRemove = cartItems.FirstOrDefault(item =>
            item.Book.BookId == id);

            if(itemToRemove != null)
            {
                if(itemToRemove.Quantity > 1)
                {
                    itemToRemove.Quantity--;
                }
                else
                {
                    cartItems.Remove(itemToRemove);
                }
            }

            HttpContext.Session.Set("Cart", cartItems);

            return RedirectToAction("ViewCart");
        }

        public IActionResult CheckOutPrice()
        {
            var cartItems = HttpContext.Session.Get<List<ShoppingCartItems>>("Cart") ?? new List<ShoppingCartItems>();
            
            foreach(var item in cartItems)
            {
                _dbContext.Purchases.Add(new Purchase
                {
                    BookId = item.Book.BookId,
                    Quantinty = item.Quantity,
                    PurchaseDate = DateTime.Now,
                    Total = (decimal?)(item.Book.Price * item.Quantity)
                }
                    );
                
            }
            _dbContext.SaveChanges();
            HttpContext.Session.Set("Cart", new List<ShoppingCartItems>());
            return RedirectToAction("Index", "Home");
        }

        public IActionResult CheckOutLendPrice()
        {
            var cartItems = HttpContext.Session.Get<List<ShoppingCartItems>>("Cart") ?? new List<ShoppingCartItems>();

            foreach (var item in cartItems)
            {
                _dbContext.Purchases.Add(new Purchase
                {
                    BookId = item.Book.BookId,
                    Quantinty = item.Quantity,
                    PurchaseDate = DateTime.Now,
                    Total = (decimal?)(item.Book.LendingPrice * item.Quantity)
                }
                    );

            }
            _dbContext.SaveChanges();
            HttpContext.Session.Set("Cart", new List<ShoppingCartItems>());
            return RedirectToAction("Index", "Home");
        }
    }
}
