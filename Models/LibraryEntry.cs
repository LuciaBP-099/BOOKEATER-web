using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace BookEater.Models
{
    public class LibraryEntry
    {
        public string? UserId { get; set; }
        public MyUser User { get; set; }

        public int BookId { get; set; }
        public Book? Book { get; set; }

        [Display(Name = "List Name")]
        public string? ListName { get; set; }
    }
}