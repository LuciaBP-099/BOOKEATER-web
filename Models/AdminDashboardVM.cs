using System.Collections.Generic;
using BookEater.Models;

namespace BookEater.Models
{
    public class AdminDashboardVM
    {
        public int TotalBooks { get; set; }
        public int TotalUsers { get; set; }
        public int TotalReviews { get; set; }
    
        public List<Book> TopRatedBooks { get; set; } = new List<Book>();

        public List<MyUser> MostActiveUsers { get; set; } = new List<MyUser>();
    }
}