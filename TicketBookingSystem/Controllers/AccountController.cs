using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using TicketBookingSystem.Data;
using TicketBookingSystem.Models;
using TicketBookingSystem.ViewModels;

namespace TicketBookingSystem.Controllers
{
    public class AccountController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AccountController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Signup()
        {
            return View();
        }

        // POST: Signup
        [HttpPost]
        public async Task<IActionResult> Signup(User user)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage);
                foreach (var error in errors)
                {
                    Console.WriteLine($"Validation Error: {error}");
                }
                return View(user);
            }

            if (_context.Users.Any(u => u.UserName == user.UserName))
            {
                ModelState.AddModelError("UserName", "Username already taken.");
                return View(user);
            }

            user.UserId = Guid.NewGuid();
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return RedirectToAction("Login");
        }


        // GET: Login
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        // POST: Login
        [HttpPost]
        public IActionResult Login(string UserName, string Password)
        {
            var user = _context.Users.FirstOrDefault(u => u.UserName == UserName && u.Password == Password);

            if (user != null)
            {
                HttpContext.Session.SetString("UserId", user.UserId.ToString());
                return RedirectToAction("Dashboard");
            }

            ViewBag.Error = "Invalid credentials!";
            return View();
        }

        public IActionResult Dashboard()
        {
            // Check if user is logged in
            string userId = HttpContext.Session.GetString("UserId");

            if (string.IsNullOrEmpty(userId))
            {
                return RedirectToAction("Login");
            }

            // Get user from the database
            var user = _context.Users.FirstOrDefault(u => u.UserId.ToString() == userId);
            if (user == null)
            {
                return RedirectToAction("Login");
            }

            var tickets = _context.Tickets
        .Include(t => t.Bus)
        .Where(t => t.UserId.ToString() == userId)
        .ToList();

            var viewModel = new UserDashboardViewModel
            {
                User = user,
                Tickets = tickets
            };

            return View(viewModel);
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }
    }
}
