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

        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Room>()
                .HasOne(r => r.Hotel)
                .WithMany(h => h.Rooms)
                .HasForeignKey(r => r.HotelId);

            modelBuilder.Entity<Booking>()
                .HasOne(b => b.Room)
                .WithMany()
                .HasForeignKey(b => b.RoomId);
        } 
    }

}
