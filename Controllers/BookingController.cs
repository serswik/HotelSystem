using HotelSystem.Data;
using HotelSystem.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace HotelSystem.Controllers
{
    public class BookingController : Controller
    {
        public readonly HotelDbContext _context;

        public BookingController(HotelDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var bookings = await _context.Bookings
                .Include(b => b.Room)
                .ThenInclude(r => r.Hotel)
                .ToListAsync();

            return View(bookings);
        }
        
        // GET: Booking/Create
        public async Task<IActionResult> Create()
        {
            var hotels = await _context.Hotels.ToListAsync();
            ViewBag.Hotels = hotels
                .Select(h => new SelectListItem
                {
                    Value = h.Id.ToString(),
                    Text = h.Name
                }).ToList();

            return View();
        }

        // POST: Booking/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("HotelId,RoomId,GuestName,GuestEmail,CheckInDate,CheckOutDate")] Booking booking)
        {
            if (ModelState.IsValid)
            {
                var room = await _context.Rooms.FindAsync(booking.RoomId);
                if(room != null)
                {
                    var days = (booking.CheckOutDate - booking.CheckInDate).Days;
                    booking.TotalPrice = room.Price * days;
                }

                _context.Add(booking);

                room.IsAvailable = false;
                _context.Update(room);

                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            var hotels = await _context.Hotels.ToListAsync();
            ViewBag.Hotels = new SelectList(hotels, "Id", "Name");

            return View(booking);
        }

        public async Task<IActionResult> GetRoomsByHotel(int hotelId)
        {
            var rooms = await _context.Rooms
                .Where(r => r.HotelId == hotelId && r.IsAvailable)
                .ToListAsync();

            var roomSelectList = rooms.Select(r => new
            {
                Value = r.Id.ToString(),
                Text = $"{r.Type} (${r.Price})",
                Price = r.Price
            }).ToList();

            return Json(roomSelectList);
        }

        // POST: Booking/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var booking = await _context.Bookings.FindAsync(id);
            if (booking != null)
            {
                
                _context.Bookings.Remove(booking);

                
                var room = await _context.Rooms.FindAsync(booking.RoomId);
                if (room != null)
                {
                    room.IsAvailable = true;
                    _context.Update(room);
                }

                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
