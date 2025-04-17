using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using TicketBookingSystem.Data;
using TicketBookingSystem.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace TicketBookingSystem.Controllers
{
    public class AdminController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AdminController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Admin Login
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        // POST: Admin Login
        [HttpPost]
        public IActionResult Login(string UserName, string Password)
        {
            var admin = _context.Admins.FirstOrDefault(a => a.UserName == UserName && a.Password == Password);
            if (admin != null)
            {
                // Store Admin ID in session
                HttpContext.Session.SetString("AdminId", admin.AdminId.ToString());
                return RedirectToAction("AdminDashBoard");
            }

            ViewBag.Error = "Invalid admin credentials!";
            return View();
        }

        // Admin Dashboard
        public async Task<IActionResult> AdminDashBoard()
        {
            string adminId = HttpContext.Session.GetString("AdminId");

            if (string.IsNullOrEmpty(adminId))
            {
                return RedirectToAction("Login");
            }

            var admin = await _context.Admins.FirstOrDefaultAsync(a => a.AdminId.ToString() == adminId);
            if (admin == null)
            {
                return RedirectToAction("Login");
            }

            // ✅ Await one-by-one to avoid DbContext concurrency
            var busCount = await _context.Busses.CountAsync();
            var userCount = await _context.Users.CountAsync();

            ViewBag.BusCount = busCount;
            ViewBag.UserCount = userCount;

            return View(admin);
        }


        // Logout
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }


        //public IActionResult BusList()
        //{
        //    var buses = _context.Busses.ToList();
        //    return View(buses);
        //}
    }
}
