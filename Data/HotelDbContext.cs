using Microsoft.EntityFrameworkCore;
using HotelSystem.Models;

namespace HotelSystem.Data
{
    public class HotelDbContext : DbContext
    {
        public HotelDbContext(DbContextOptions<HotelDbContext> options) : base(options) { }

        public DbSet<Hotel> Hotels { get; set; }
        public DbSet<Room> Rooms {  get; set; }
        public DbSet<Booking> Bookings { get; set; }

        /* Filling up database info
         * protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Room>().HasData(
                )
        }
        */ 
    }

}
