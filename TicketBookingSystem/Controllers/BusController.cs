using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using TicketBookingSystem.Data;
using TicketBookingSystem.Models;

namespace TicketBookingSystem.Controllers
{
    public class BusController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BusController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            string adminId = HttpContext.Session.GetString("AdminId");
            if (string.IsNullOrEmpty(adminId))
            {
                return RedirectToAction("Login","Admin");
            }
            var buses = _context.Busses.ToList();
            return View(buses);
        }

        [HttpPost]
        public async Task<IActionResult> AddBus(Bus bus)
        {
            if (ModelState.IsValid)
            {
                bus.BusId = Guid.NewGuid();

                // Initialize seats
                var seats = new List<List<string>>();
                for (int i = 0; i < 10; i++)
                {
                    seats.Add(Enumerable.Repeat("O", 4).ToList());
                }

                // Set both properties explicitly
                bus.Seats = seats;
                bus.SeatsJson = JsonSerializer.Serialize(seats);

                _context.Busses.Add(bus);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View("Bus", _context.Busses.ToList());
        }



        // Search Buses
        [HttpGet]
        public async Task<IActionResult> SearchBuses(string StartPoint, string EndPoint)
        {
            var buses = await _context.Busses
                .Where(b => b.StartingPoint == StartPoint && b.EndingPoint == EndPoint)
                .ToListAsync();

            if (buses.Any())
            {
                return View("BusList", buses); // Show filtered buses
            }
            else
            {
                ViewBag.Message = "No buses found for the selected route.";
                return View("BusList", new List<Bus>()); // Return empty list with message
            }
        }
        [HttpGet]
        public IActionResult BusSeatView(Guid id)
        {
            var bus = _context.Busses.FirstOrDefault(b => b.BusId == id);

            if (bus == null)
                return NotFound();

            return View(bus);
        }

        [HttpPost]
        public async Task<IActionResult> BookSeat(Guid BusId, string SeatNumber)
        {
            var userId = HttpContext.Session.GetString("UserId");
            if (string.IsNullOrEmpty(userId))
                return RedirectToAction("Login", "Account");

            var bus = _context.Busses.FirstOrDefault(b => b.BusId == BusId);
            if (bus == null)
                return NotFound();

            var seats = bus.Seats;
            int row = SeatNumber[0] - 'A';
            int col = int.Parse(SeatNumber.Substring(1)) - 1;

            if (seats[row][col] == "X")
            {
                TempData["Error"] = "Seat already booked.";
                return RedirectToAction("BusSeatView", new { id = BusId });
            }

            seats[row][col] = "X";
            bus.Seats = seats;
            _context.Update(bus);

            var ticket = new Ticket
            {
                TicketId = Guid.NewGuid(),
                BusId = BusId,
                UserId = Guid.Parse(userId),
                SeatNumber = SeatNumber,
                BookingDate = DateTime.Now,
                Price = bus.Price
            };

            _context.Tickets.Add(ticket);
            await _context.SaveChangesAsync();

            return RedirectToAction("Dashboard", "Account");
        }
        [HttpGet]
        public IActionResult UpdateBus(Guid id)
        {
            var bus = _context.Busses.FirstOrDefault(b => b.BusId == id);
            if (bus == null)
                return NotFound();
            return View(bus);
        }

        [HttpPost]
        public async Task<IActionResult>UpdateBus(Bus bs)
        {
            if (ModelState.IsValid)
            {
                var existingBus = _context.Busses.FirstOrDefault(b => b.BusId == bs.BusId);
                if (existingBus == null) return NotFound();

                existingBus.BusName = bs.BusName;
                existingBus.Model = bs.Model;
                existingBus.Time = bs.Time;
                existingBus.StartingPoint = bs.StartingPoint;
                existingBus.EndingPoint = bs.EndingPoint;
                existingBus.Price = bs.Price;

                _context.Update(existingBus);
                await _context.SaveChangesAsync();

                return RedirectToAction("Index");
            }

            return View(bs);
        }
        [HttpPost]
        public async Task<IActionResult> DeleteBus(Guid id)
        {
            var bus = await _context.Busses.FindAsync(id);
            if (bus == null)
                return NotFound();
            _context.Busses.Remove(bus);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
         }



    }
}
