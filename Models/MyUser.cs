using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace BookEater.Models
{
    public class MyUser : IdentityUser
    {
        public string? FullName { get; set; }
        public DateTime CreationDate { get; set; } = DateTime.Now;

        public ICollection<LibraryEntry> UserBooks { get; set; } = new List<LibraryEntry>();

        public ICollection<Review> Reviews { get; set; } = new List<Review>();
    }
}