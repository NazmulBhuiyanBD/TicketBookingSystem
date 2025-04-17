using TicketBookingSystem.Models;

namespace TicketBookingSystem.ViewModels
{
    public class UserDashboardViewModel
    {
        public User User { get; set; }
        public List<Ticket> Tickets { get; set; }
    }
}
