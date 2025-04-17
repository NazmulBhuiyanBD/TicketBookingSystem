using System.ComponentModel.DataAnnotations;

namespace TicketBookingSystem.Models
{
    public class User
    {
        [Key]
        public Guid UserId { get; set; }

        [Required(ErrorMessage = "Username is required")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public string? Password { get; set; }

        public string? FullName { get; set; }

        [Phone(ErrorMessage = "Invalid phone number")]
        public required string Phone { get; set; }
    }
}
