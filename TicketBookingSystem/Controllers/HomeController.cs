using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using TicketBookingSystem.Models;

namespace TicketBookingSystem.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    [HttpGet]
    public IActionResult Index()
    {
        return View();
    }
    [HttpPost]
    public IActionResult SearchBus (string StartPoint, string EndPoint)
    {
        if (!string.IsNullOrEmpty(StartPoint) && !string.IsNullOrEmpty(EndPoint))
        {
            ViewBag.Message = $"Searching buses from {StartPoint} to {EndPoint}...";
        }
        else
        {
            ViewBag.Message = "Please enter both locations.";
        }
        return RedirectToAction("SearchBuses", "Bus", new { StartPoint, EndPoint });
    }
    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
