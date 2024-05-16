using BooksManagement.Data;
using BooksManagement.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BooksManagement.Controllers
{
    [Authorize(Roles.RolesAvailable.Role_Admin)]
    public class CategoryController : Controller
    {
        private readonly ApplicationDBContext _dbContext;
        public CategoryController(ApplicationDBContext dbContext)
        {
            _dbContext = dbContext;
        }
        public IActionResult Index()
        {
            List<Category> objCategoryList = _dbContext.Categories.ToList();
            return View(objCategoryList);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Category obj)
        { 
            if(ModelState.IsValid)
            {
                _dbContext.Categories.Add(obj);
                _dbContext.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
            
        }

        public IActionResult Edit( int? CategoryId)
        {
            if(CategoryId == null || CategoryId == 0)
            {
                return NotFound();
            }
            //Category category = _dbContext.Categories.Find(id);
            Category? category = _dbContext.Categories.FirstOrDefault(u=> u.CategoryId== CategoryId);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }

        [HttpPost]
        public IActionResult Edit(Category obj)
        {
            if (ModelState.IsValid)
            {
                _dbContext.Categories.Update(obj);
                _dbContext.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
        }

        public IActionResult Delete(int? CategoryId)
        {
            if (CategoryId == null || CategoryId == 0)
            {
                return NotFound();
            }
            //Category category = _dbContext.Categories.Find(id);
            Category? category = _dbContext.Categories.FirstOrDefault(u => u.CategoryId == CategoryId);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }
        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePost(int? CategoryId)
        {
             Category? obj = _dbContext.Categories.FirstOrDefault(c => c.CategoryId == CategoryId); 
            if (obj == null)
            {
                return NotFound();
            }
            _dbContext.Categories.Remove(obj);
            _dbContext.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
