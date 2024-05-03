using System.ComponentModel.DataAnnotations;

namespace BooksManagement.Models
{
    public class Category
    {
        [Key]
        public int CategoryId { get; set; }
        [Required]
        public string Name { get; set; }
    }
}
