using Microsoft.AspNetCore.Mvc;
using TicketBookingSystem.Data;

namespace TicketBookingSystem.Controllers
{
    public class UserController : Controller
    {
        private readonly ApplicationDbContext _context;
        public UserController(ApplicationDbContext context)
        {
            this._context = context;
        }
        public IActionResult Index()
        {
            var user = _context.Users.ToList();
            return View(user);
        }
       
    }
}
