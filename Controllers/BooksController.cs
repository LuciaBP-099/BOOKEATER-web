using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BookEater.Data;
using BookEater.Models;

namespace BookEater.Controllers
{
    [Authorize]
    public class BooksController : Controller
    {
        private readonly ApplicationDbContext _db;

        public BooksController(ApplicationDbContext context)
        {
            _db = context;
        }

        public async Task<IActionResult> Index()
        {
            var userId = GetCurrentUserId();
            var libraryEntries = await _db.UserBooks
                .Include(x => x.Book)
                .Where(x => x.UserId == userId)
                .OrderBy(x => x.ListName)
                .ToListAsync();

            return View(libraryEntries);
        }

        [AllowAnonymous]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var book = await _db.Books
                .Include(b => b.Reviews)
                .ThenInclude(r => r.User)
                .FirstOrDefaultAsync(m => m.BookId == id);

            if (book == null) return NotFound();

            return View(book);
        }

        [HttpGet]
        public async Task<IActionResult> Search(string searchString)
        {
            var books = _db.Books.AsQueryable();

            if (!string.IsNullOrEmpty(searchString))
            {
                books = books.Where(s => s.Title.Contains(searchString) || s.Author.Contains(searchString));
            }
            
            return View(await books.OrderBy(b => b.Title).Take(20).ToListAsync());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddToLibrary(int bookId, string? listName, int? rating)
        {
            var userId = GetCurrentUserId();

            var exists = await _db.UserBooks.AnyAsync(ub => ub.UserId == userId && ub.BookId == bookId);
            if (!exists)
            {
                _db.UserBooks.Add(new LibraryEntry
                {
                    UserId = userId,
                    BookId = bookId,
                    ListName = string.IsNullOrEmpty(listName) ? "General" : listName
                });
            }

            if (rating.HasValue && rating > 0)
            {
                await SaveOrUpdateReview(bookId, userId, rating.Value, "Quick rating.");
            }

            await _db.SaveChangesAsync();
            await UpdateBookAverage(bookId);

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();
            var book = await _db.Books.FindAsync(id);
            if (book == null) return NotFound();

            var userId = GetCurrentUserId();
            var userEntry = await _db.UserBooks.FirstOrDefaultAsync(x => x.BookId == id && x.UserId == userId);
            var userReview = await _db.Reviews.FirstOrDefaultAsync(x => x.BookId == id && x.UserId == userId);

            ViewData["UserListName"] = userEntry?.ListName ?? "General";
            ViewData["UserRating"] = userReview?.Rating ?? 0;

            return View(book);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, string? ListName, int UserRating)
        {
            var userId = GetCurrentUserId();

            // Update List
            var userEntry = await _db.UserBooks.FirstOrDefaultAsync(x => x.BookId == id && x.UserId == userId);
            if (userEntry != null)
            {
                userEntry.ListName = string.IsNullOrEmpty(ListName) ? "General" : ListName;
                _db.Update(userEntry);
            }
            else
            {
                _db.UserBooks.Add(new LibraryEntry { UserId = userId, BookId = id, ListName = ListName ?? "General" });
            }

            // Update Rating
            if (UserRating > 0)
            {
                await SaveOrUpdateReview(id, userId, UserRating, "Rating from edit.");
            }
            
            await _db.SaveChangesAsync();
            await UpdateBookAverage(id);

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> WriteReview(int? id)
        {
            if (id == null) return NotFound();
            var book = await _db.Books.FindAsync(id);
            if (book == null) return NotFound();
            return View(book);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> WriteReview(int BookId, int Rating, string Content)
        {
            var userId = GetCurrentUserId();
            if (Rating < 1) Rating = 1;

            await SaveOrUpdateReview(BookId, userId, Rating, Content);

            await _db.SaveChangesAsync();
            await UpdateBookAverage(BookId);

            return RedirectToAction("Details", new { id = BookId });
        }

        [HttpGet]
        public async Task<IActionResult> DeleteReview(int? id)
        {
            if (id == null) return NotFound();

            var review = await _db.Reviews
                .Include(r => r.Book)
                .FirstOrDefaultAsync(r => r.ReviewId == id);

            if (review == null) return NotFound();
            if (review.UserId != GetCurrentUserId()) return RedirectToAction(nameof(Index));

            return View(review);
        }

        [HttpPost, ActionName("DeleteReview")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteReviewConfirmed(int id)
        {
            var review = await _db.Reviews.FindAsync(id);
            if (review != null && review.UserId == GetCurrentUserId())
            {
                int bookId = review.BookId;
                _db.Reviews.Remove(review);
                await _db.SaveChangesAsync();
                await UpdateBookAverage(bookId);
            }
            return RedirectToAction(nameof(MyReviews));
        }

        // Removes book from User's Library
        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var book = await _db.Books.FirstOrDefaultAsync(m => m.BookId == id);
            if (book == null) return NotFound();

            var userId = GetCurrentUserId();
            var userHasBook = await _db.UserBooks.AnyAsync(ub => ub.BookId == id && ub.UserId == userId);

            if (!userHasBook) return RedirectToAction(nameof(Index));

            return View(book);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int BookId)
        {
            var entry = await _db.UserBooks
                .FirstOrDefaultAsync(x => x.BookId == BookId && x.UserId == GetCurrentUserId());

            if (entry != null)
            {
                _db.UserBooks.Remove(entry);
                await _db.SaveChangesAsync();
            }
            
            return RedirectToAction(nameof(Index));
        }

        // Admin-only hard delete from database
        [HttpGet]
        public async Task<IActionResult> AdminDeleteBook(int? id)
        {
            if (User.Identity.Name != "administratorBookEater@gmail.com") return RedirectToAction(nameof(Index));
            if (id == null) return NotFound();
            var book = await _db.Books.FirstOrDefaultAsync(m => m.BookId == id);
            return book == null ? NotFound() : View("Delete", book); 
        }

        [HttpPost, ActionName("AdminDeleteBook")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AdminDeleteBookConfirmed(int id)
        {
            if (User.Identity.Name != "administratorBookEater@gmail.com") return RedirectToAction(nameof(Index));
            var book = await _db.Books.FindAsync(id);
            if (book != null)
            {
                var links = _db.UserBooks.Where(x => x.BookId == id);
                _db.UserBooks.RemoveRange(links);
                var reviews = _db.Reviews.Where(x => x.BookId == id);
                _db.Reviews.RemoveRange(reviews);
                _db.Books.Remove(book);
                await _db.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        // --- Helpers ---

        private async Task SaveOrUpdateReview(int bookId, string userId, int rating, string content)
        {
            var existingReview = await _db.Reviews
                .FirstOrDefaultAsync(r => r.UserId == userId && r.BookId == bookId);

            if (existingReview != null)
            {
                existingReview.Rating = rating;
                existingReview.Content = content;
                existingReview.DatePosted = DateTime.Now;
                _db.Update(existingReview);
            }
            else
            {
                var newReview = new Review
                {
                    BookId = bookId,
                    UserId = userId,
                    Rating = rating,
                    Content = content,
                    DatePosted = DateTime.Now
                };
                _db.Reviews.Add(newReview);
            }
        }

        private async Task UpdateBookAverage(int bookId)
        {
            var book = await _db.Books.Include(b => b.Reviews).FirstOrDefaultAsync(b => b.BookId == bookId);
            if (book != null)
            {
                if (book.Reviews.Any())
                    book.Rating = book.Reviews.Average(r => r.Rating);
                else
                    book.Rating = 0;
                
                _db.Update(book);
                await _db.SaveChangesAsync();
            }
        }

        private string GetCurrentUserId() => User.FindFirstValue(ClaimTypes.NameIdentifier);
        
        public async Task<IActionResult> MyReviews()
        {
            var userId = GetCurrentUserId();
            var myReviews = await _db.Reviews.Include(r => r.Book).Where(r => r.UserId == userId).ToListAsync();
            return View(myReviews);
        }
        
        public async Task<IActionResult> Community() => View(await _db.Books.Include(b => b.Reviews).ToListAsync());
    }
}