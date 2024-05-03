using Microsoft.AspNetCore.Mvc;

namespace BooksManagement.Controllers
{
    public class CategoryController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
