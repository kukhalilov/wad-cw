using System;
using System.ComponentModel.DataAnnotations;

namespace WebAPI.Models
{
    public class Book
    {
        public int Id { get; set; }


        [Required(ErrorMessage = "Title is required")]
        public string Title { get; set; }


        [Required(ErrorMessage = "Author is required")]
        public Author Author { get; set; }


        [Required(ErrorMessage = "Publish date is required")]
        [DataType(DataType.Date)]
        public DateTime PublishDate { get; set; }


        [Required(ErrorMessage = "Description is required")]
        public string Description { get; set; }


        [Range(1, int.MaxValue, ErrorMessage = "Page count must be a positive integer")]
        public int PageCount { get; set; }


        public bool IsAvailable { get; set; }
    }
}
