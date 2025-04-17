using System.ComponentModel.DataAnnotations;

namespace TicketBookingSystem.Models
{
    public class Admin
    {
        [Key]
        public Guid AdminId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
