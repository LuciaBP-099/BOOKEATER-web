using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BookEater.Data;
using BookEater.Models;
using BookEater.Filters;

namespace BookEater.Controllers.Api
{
    [Route("api")] 
    [ApiController]
    [ApiKeyAuth] 
    public class AppApiController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public AppApiController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet("books")]
        public async Task<ActionResult<IEnumerable<object>>> GetBooks()
        {
            var books = await _context.Books
                .Select(b => new {
                    b.BookId,
                    b.Title,
                    b.Author,
                    b.Genre,
                    b.Rating
                })
                .ToListAsync();
            return Ok(books);
        }

        [HttpGet("reviews/{bookId}")]
        public async Task<ActionResult<IEnumerable<object>>> GetReviews(int bookId)
        {
            var reviews = await _context.Reviews
                .Where(r => r.BookId == bookId)
                .Include(r => r.User)
                .Select(r => new {
                    Id = r.ReviewId, 
                    r.Rating,
                    Comment = r.Content,   
                    UserName = r.User.UserName ?? "Anonymous",
                    Date = r.DatePosted 
                })
                .ToListAsync();
            return Ok(reviews);
        }

        [HttpPost("reviews")]
        public async Task<ActionResult> PostReview([FromBody] ReviewDto reviewDto)
        {
            if (reviewDto == null) return BadRequest("Invalid Data");

            var user = await _context.Users.FirstOrDefaultAsync();
            if (user == null) return BadRequest("No users in database.");

            var review = new Review {
                BookId = reviewDto.BookId,
                UserId = user.Id,
                Rating = reviewDto.Rating,
                Content = reviewDto.Comment,
                DatePosted = DateTime.Now 
            };

            _context.Reviews.Add(review);

            var book = await _context.Books.Include(b => b.Reviews)
                .FirstOrDefaultAsync(b => b.BookId == reviewDto.BookId);

            if (book != null) {
                var currentSum = book.Reviews.Sum(r => r.Rating); 
                var count = book.Reviews.Count + 1;
                book.Rating = Math.Round((double)(currentSum + reviewDto.Rating) / count, 1);
            }

            await _context.SaveChangesAsync();
            return Ok(new { message = "Review saved" });
        }
    }

    public class ReviewDto {
        public int BookId { get; set; }
        public string UserName { get; set; }
        public int Rating { get; set; }
        public string Comment { get; set; }
    }
}
