using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BookEater.Data;
using BookEater.Models;
using System.Linq;
using System.Threading.Tasks;

namespace BookEater.Controllers
{
    [Authorize] 
    public class AdminController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<MyUser> _userManager;

        public AdminController(ApplicationDbContext context, UserManager<MyUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        private bool IsAdmin()
        {
            return User.Identity.Name == "administratorBookEater@gmail.com";
        }

        public async Task<IActionResult> Index()
        {
            if (!IsAdmin()) return RedirectToAction("Index", "Home");

            var dashboardData = new AdminDashboardVM
            {
                TotalBooks = await _context.Books.CountAsync(),
                TotalUsers = await _userManager.Users.CountAsync(),
                TotalReviews = await _context.Reviews.CountAsync(),
                
                TopRatedBooks = await _context.Books
                    .OrderByDescending(b => b.Rating)
                    .Take(5)
                    .ToListAsync(),

                MostActiveUsers = await _userManager.Users
                    .Include(u => u.UserBooks)
                    .OrderByDescending(u => u.UserBooks.Count)
                    .Take(5)
                    .ToListAsync()
            };

            return View(dashboardData);
        }

        public async Task<IActionResult> AllUsers()
        {
            if (!IsAdmin()) return RedirectToAction("Index", "Home");

            var users = await _userManager.Users
                .Include(u => u.UserBooks) 
                .ToListAsync();

            return View(users);
        }


        public async Task<IActionResult> AllBooks()
        {
            if (!IsAdmin()) return RedirectToAction("Index", "Home");

            var books = await _context.Books
                .Include(b => b.Reviews)
                .ToListAsync();

            return View(books);
        }

        public async Task<IActionResult> Reviews(int? id)
        {
            if (!IsAdmin()) return RedirectToAction("Index", "Home");
            if (id == null) return NotFound();

            var book = await _context.Books
                .Include(b => b.Reviews)
                .ThenInclude(r => r.User)
                .FirstOrDefaultAsync(b => b.BookId == id);

            if (book == null) return NotFound();

            return View(book);
        }

        public async Task<IActionResult> DeleteReview(int? id)
        {
            if (!IsAdmin()) return RedirectToAction("Index", "Home");
            if (id == null) return NotFound();

            var review = await _context.Reviews
                .Include(r => r.Book)
                .Include(r => r.User)
                .FirstOrDefaultAsync(r => r.ReviewId == id);

            if (review == null) return NotFound();

            return View(review);
        }

        [HttpPost, ActionName("DeleteReview")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteReviewConfirmed(int id)
        {
            if (!IsAdmin()) return RedirectToAction("Index", "Home");

            var review = await _context.Reviews.FindAsync(id);
            if (review != null)
            {
                int bookId = review.BookId;
                _context.Reviews.Remove(review);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Reviews), new { id = bookId });
            }
            return RedirectToAction(nameof(AllBooks));
        }
    }
}