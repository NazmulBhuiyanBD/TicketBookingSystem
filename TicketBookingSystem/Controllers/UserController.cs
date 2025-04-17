using Microsoft.AspNetCore.Mvc;

namespace TicketBookingSystem.Controllers
{
    public class UserController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
