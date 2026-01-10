using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using BookEater.Models;
using System.Threading.Tasks;

namespace BookEater.Controllers
{
    [Route("account")]
    public class LoginController : Controller
    {
        private readonly SignInManager<MyUser> _authManager; 

        public LoginController(SignInManager<MyUser> manager)
        {
            _authManager = manager;
        }

        [HttpGet("login")]
        public IActionResult Login(string returnUrl = "/")
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View("~/Views/Account/Login.cshtml", new LoginVM());
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginVM input, string returnUrl = "/")
        {
            if (!ModelState.IsValid)
                return View("~/Views/Account/Login.cshtml", input);

            var result = await _authManager.PasswordSignInAsync(input.Email, input.Password, isPersistent: false, lockoutOnFailure: false);

            if (result.Succeeded)
            {
                if (string.IsNullOrEmpty(returnUrl) || returnUrl == "/")
                    return RedirectToAction("Index", "Books");

                return LocalRedirect(returnUrl);
            }

            ModelState.AddModelError(string.Empty, "Incorrect email or password.");
            return View("~/Views/Account/Login.cshtml", input);
        }

        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            await _authManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}