using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BookEater.Data;
using BookEater.Models;

namespace BookEater.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppApiController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public AppApiController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/AppApi/books
        [HttpGet("books")]
        public async Task<ActionResult<IEnumerable<object>>> GetBooks()
        {
            var books = await _context.Books
                .Select(b => new
                {
                    Id = b.BookId,
                    b.Title,
                    b.Author,
                    b.Genre,
                    b.Rating
                })
                .ToListAsync();

            return Ok(books);
        }

        // GET: api/AppApi/reviews/{bookId}
        [HttpGet("reviews/{bookId}")]
        public async Task<ActionResult<IEnumerable<object>>> GetReviews(int bookId)
        {
            var reviews = await _context.Reviews
                .Where(r => r.BookId == bookId)
                .Include(r => r.User)
                .Select(r => new
                {
                    Id = r.ReviewId,       
                    r.Rating,
                    Comment = r.Content,   
                    UserName = r.User.UserName,
                    Date = r.DatePosted    
                })
                .ToListAsync();

            return Ok(reviews);
        }

        // POST: api/AppApi/reviews
        [HttpPost("reviews")]
        public async Task<ActionResult> PostReview([FromBody] ReviewDto reviewDto)
        {
            if (reviewDto == null) return BadRequest("Invalid Data");

            var user = await _context.Users.FirstOrDefaultAsync(u => u.UserName == reviewDto.UserName);
            if (user == null) return BadRequest("User not found");

            var review = new Review
            {
                BookId = reviewDto.BookId,
                UserId = user.Id,
                Rating = reviewDto.Rating,
                Content = reviewDto.Comment, 
                DatePosted = DateTime.Now    
            };

            _context.Reviews.Add(review);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Review saved" });
        }
    }

    public class ReviewDto
    {
        public int BookId { get; set; }
        public string UserName { get; set; }
        public int Rating { get; set; }
        public string Comment { get; set; }
    }
}