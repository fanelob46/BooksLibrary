using BooksManagement.Data;
using BooksManagement.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Net;

namespace BooksManagement.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDBContext _dbContext;

        public HomeController(ILogger<HomeController> logger, ApplicationDBContext dBContext)
        {
            _logger = logger;
            _dbContext = dBContext;
        }

        public IActionResult Index()
        {
            IEnumerable<Book> booksList = _dbContext.Books.ToList();
            return View(booksList);
        }

        public IActionResult Details(int BookId)
        {
            Book? book = _dbContext.Books.FirstOrDefault(u => u.BookId == BookId);
            return View(book);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
