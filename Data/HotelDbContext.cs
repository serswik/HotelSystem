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
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Room>()
                .Property(r => r.Price)
                .HasColumnType("decimal(18,0)");

            modelBuilder.Entity<Booking>()
                .Property(b => b.TotalPrice)
                .HasColumnType("decimal(18,0)");

            modelBuilder.Entity<Hotel>().HasData(
                new Hotel { Id = 1, Name = "Oceanview Resort", Address = "123 Beach Road", Description = "A seaside resort with stunning ocean views." },
                new Hotel { Id = 2, Name = "Mountain Escape", Address = "456 Hilltop Ave", Description = "A quiet retreat in the mountains." }
            );

            modelBuilder.Entity<Room>().HasData(
                new Room { Id = 1, HotelId = 1, Type = RoomType.Single, Price = 100.00m, IsAvailable = true, Description = "A cozy single room with a sea view." },
                new Room { Id = 2, HotelId = 1, Type = RoomType.Double, Price = 150.00m, IsAvailable = true, Description = "A spacious double room with a balcony." },
                new Room { Id = 3, HotelId = 2, Type = RoomType.Suite, Price = 250.00m, IsAvailable = true, Description = "A luxurious suite with mountain views." }
            );

            modelBuilder.Entity<Booking>().HasData(
                new Booking
                {
                    Id = 1,
                    RoomId = 1,
                    GuestName = "John Doe",
                    GuestEmail = "johndoe@example.com",
                    CheckInDate = new DateTime(2024, 12, 20),
                    CheckOutDate = new DateTime(2024, 12, 25),
                    TotalPrice = 500.00m
                },
                new Booking
                {
                    Id = 2,
                    RoomId = 2,
                    GuestName = "Jane Smith",
                    GuestEmail = "janesmith@example.com",
                    CheckInDate = new DateTime(2024, 12, 22),
                    CheckOutDate = new DateTime(2024, 12, 27),
                    TotalPrice = 750.00m
                }
            );
        } 
    }

}
