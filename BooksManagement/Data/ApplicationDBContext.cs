using BooksManagement.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BooksManagement.Data
{
    public class ApplicationDBContext: IdentityDbContext<IdentityUser>
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options) 
        { 

        }
      public DbSet<Category> Categories { get; set; }
      public DbSet<Book> Books { get; set; }
      public DbSet<ShoppingCartItems> Items { get; set; }
        public DbSet<Purchase> Purchases { get; set; }

        public DbSet<ApplicationUser> Users { get; set; }
     
    }
}
