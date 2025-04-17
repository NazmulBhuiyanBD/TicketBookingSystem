using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;

namespace TicketBookingSystem.Models
{
    public class Bus
    {
        [Key]
        public Guid BusId { get; set; }

        [Required]
        public string BusName { get; set; }

        [Required]
        public string Model { get; set; }

        [Required]
        public DateTime Time { get; set; }

        [Required]
        public string StartingPoint { get; set; }

        [Required]
        public string EndingPoint { get; set; }

        [Required]
        public decimal Price { get; set; }

        public string SeatsJson { get; set; } = JsonSerializer.Serialize(new List<List<string>>());

        [NotMapped]
        public List<List<string>> Seats
        {
            get => string.IsNullOrEmpty(SeatsJson) ? new List<List<string>>() : JsonSerializer.Deserialize<List<List<string>>>(SeatsJson);
            set => SeatsJson = JsonSerializer.Serialize(value);
        }
    }

}
