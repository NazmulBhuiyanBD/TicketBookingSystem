using Microsoft.EntityFrameworkCore;
using TicketBookingSystem.Models;

namespace TicketBookingSystem.Data
{
    public class ApplicationDbContext :DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext>Options):base(Options)
        {
            
        }
        public DbSet<Admin> Admins { get; set; }
        public DbSet<Bus> Busses { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
    }
}
