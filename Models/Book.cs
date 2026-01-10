using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BookEater.Models
{
    public class Book
    {
        public int BookId { get; set; }

        [Required(ErrorMessage = "The title is required!")] 
        [Display(Name = "Book Title")] 
        public string? Title { get; set; }

        [Display(Name = "Genre")]
        public string? Genre { get; set; }

        [Display(Name = "Author / Writer")]
        public string? Author { get; set; }

        [Range(0.0, 5.0, ErrorMessage = "It should be between 0 and 5")]
        [Display(Name = "Rating (0-5)")]
        public double? Rating { get; set; }

        public ICollection<LibraryEntry> UserBooks { get; set; } = new List<LibraryEntry>();
        public ICollection<Review> Reviews { get; set; } = new List<Review>();
    }
}