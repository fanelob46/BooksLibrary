using BooksManagement.Data;
using BooksManagement.Models;
using BooksManagement.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace BooksManagement.Controllers
{
    public class BookController : Controller
    {
        private readonly ApplicationDBContext _dbContext;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public BookController(ApplicationDBContext dbContext, IWebHostEnvironment webHostEnvironment)
        {
            _dbContext = dbContext;
            _webHostEnvironment = webHostEnvironment;
        }
        public IActionResult Index()
        {
            List<Book> objBookList = _dbContext.Books.ToList();

            return View(objBookList);
        }

        public IActionResult Create()
        {
            BookViewModel bookViewModel = new()
            {
                CategoryList = _dbContext.Categories.ToList()
                .Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.CategoryId.ToString(),
                }),
                Book = new Book()
            };
            return View(bookViewModel);
        } 

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(BookViewModel obj, IFormFile? file)
        {
            if (ModelState.IsValid)
            {
                string wwwRootPath = _webHostEnvironment.WebRootPath;
                if(file != null)
                {
                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                    string bookPath = Path.Combine(wwwRootPath, @"images\books");

                    using (var fileStream = new FileStream(Path.Combine(bookPath,fileName),FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                    }
                    obj.Book.ImageUrl = @"\images\books\" + fileName;
                }
                _dbContext.Books.Add(obj.Book);
                _dbContext.SaveChanges();
                return RedirectToAction("Index");
            }
           return View();

        }

        public IActionResult Edit(int? BookId, IFormFile? file)
        {
            if (BookId == null || BookId == 0)
            {
                
                return NotFound();
            }
            //Book Book = _dbContext.Books.Find(id);
            Book? Book = _dbContext.Books.FirstOrDefault(u => u.BookId == BookId);
        
            if (Book == null)
            {
               
                return NotFound();
            }
            return View(Book);
        }

        [HttpPost]
        public IActionResult Edit(Book obj)
        {
            if (ModelState.IsValid)
            {
                _dbContext.Books.Update(obj);
                _dbContext.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
        }

        public IActionResult Delete(int? BookId)
        {
            if (BookId == null || BookId == 0)
            {
                return NotFound();
            }
            //Book Book = _dbContext.Books.Find(id);
            Book? Book = _dbContext.Books.FirstOrDefault(u => u.BookId == BookId);
            if (Book == null)
            {
                return NotFound();
            }
            return View(Book);
        }
        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePost(int? BookId)
        {
            Book? obj = _dbContext.Books.FirstOrDefault(c => c.BookId == BookId);
            if (obj == null)
            {
                return NotFound();
            }
            _dbContext.Books.Remove(obj);
            _dbContext.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}

