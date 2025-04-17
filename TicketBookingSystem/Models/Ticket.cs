using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TicketBookingSystem.Models;

namespace TicketBookingSystem.Models
{
    public class Ticket
    {
        [Key]
        public Guid TicketId { get; set; }

        [ForeignKey("User")]
        public Guid UserId { get; set; }
        public User? User { get; set; }

        [ForeignKey("Buss")]
        public Guid BusId { get; set; }
        public Bus? Bus { get; set; }

        public string SeatNumber { get; set; } = string.Empty;

        [Required]
        public DateTime BookingDate { get; set; }

        public decimal Price { get; set; }
    }
}
