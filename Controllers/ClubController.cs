using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BookEater.Data;
using BookEater.Models;

namespace BookEater.Controllers
{
    [Authorize]
    public class ClubController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<MyUser> _userManager;

        public ClubController(ApplicationDbContext context, UserManager<MyUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            // 1. Define current month (ignore day/time)
            var today = DateTime.Now;
            var startOfMonth = new DateTime(today.Year, today.Month, 1);

            // 2. Check if a pick already exists for this month
            var activePick = await _context.ClubPicks
                .Include(cp => cp.Book)
                .Include(cp => cp.Comments)
                .ThenInclude(c => c.User)
                .FirstOrDefaultAsync(cp => cp.Month.Month == today.Month && cp.Month.Year == today.Year);

            // 3. If none exists, automatically select a random book
            if (activePick == null)
            {
                var randomBook = await _context.Books
                    .OrderBy(b => Guid.NewGuid())
                    .FirstOrDefaultAsync();

                if (randomBook != null)
                {
                    activePick = new ClubPick
                    {
                        BookId = randomBook.BookId,
                        Month = startOfMonth,
                        Topic = "Monthly Random Selection: What are your thoughts?",
                        Book = randomBook 
                    };

                    _context.ClubPicks.Add(activePick);
                    await _context.SaveChangesAsync();
                }
            }
            return View(activePick);
        }

        [HttpPost]
        public async Task<IActionResult> PostComment(int ClubPickId, string Content)
        {
            if (string.IsNullOrWhiteSpace(Content)) return RedirectToAction("Index");

            var comment = new ClubComment
            {
                ClubPickId = ClubPickId,
                Content = Content,
                UserId = _userManager.GetUserId(User),
                PostedDate = DateTime.Now
            };

            _context.ClubComments.Add(comment);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        // Admin Only: Manually create a pick (GET)
        public IActionResult Create()
        {
            if (User.Identity.Name != "administratorBookEater@gmail.com") return RedirectToAction("Index");

            ViewData["BookId"] = new SelectList(_context.Books, "BookId", "Title");
            return View();
        }

        // Admin Only: Manually create a pick (POST)
        [HttpPost]
        public async Task<IActionResult> Create(ClubPick pick)
        {
             if (User.Identity.Name != "administratorBookEater@gmail.com") return RedirectToAction("Index");

            pick.Month = DateTime.Now;
            _context.Add(pick);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}