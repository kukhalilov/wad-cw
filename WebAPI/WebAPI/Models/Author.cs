using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebAPI.Models
{
    public class Author
    {
        public Author() 
        {
            this.Books = new List<Book>();
        }
        public int AuthorId { get; set; }

        [Required(ErrorMessage = "Name is required")]
        [MaxLength(50, ErrorMessage = "Name cannot exceed 100 characters")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Surname is required")]
        [MaxLength(50, ErrorMessage = "Surname cannot exceed 100 characters")]
        public string Surname { get; set; }

        public virtual ICollection<Book> Books { get; set; }
    }
}
