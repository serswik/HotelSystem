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

        // GET: Booking/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if(id == null)
            {
                return NotFound();
            }

            var booking = await _context.Bookings
                .Include(b => b.Room)
                .ThenInclude(r => r.Hotel)
                .FirstOrDefaultAsync(b => b.Id == id);

            if(booking == null)
            {
                return NotFound();
            }

            ViewBag.Hotel = booking.Room.Hotel;

            var avaliableRooms = await _context.Rooms
                .Where(r => r.HotelId == booking.Room.HotelId && (r.IsAvailable || r.Id == booking.RoomId))
                .ToListAsync();

            ViewBag.Rooms = avaliableRooms;

            return View(booking);
        }

        // POST: Booking/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,RoomId,GuestName,GuestEmail,CheckInDate,CheckOutDate")] Booking booking)
        {
            if(id != booking.Id)
            {
                return NotFound();
            }

            if(ModelState.IsValid)
            {
                try
                {
                    var room = await _context.Rooms.FindAsync(booking.RoomId);

                    if (room != null)
                    {
                        var days = (booking.CheckOutDate - booking.CheckInDate).Days;
                        booking.TotalPrice = room.Price * days;

                        var oldBooking = await _context.Bookings
                            .Include(b => b.Room)
                            .FirstOrDefaultAsync(b => b.Id == booking.Id);

                        if (oldBooking != null)
                        {
                            _context.Entry(oldBooking).State = EntityState.Detached;

                            if(oldBooking.RoomId != booking.RoomId)
                            {
                                oldBooking.Room.IsAvailable = true;
                                room.IsAvailable = false;
                            }
                        }

                        _context.Update(booking);
                        await _context.SaveChangesAsync();
                    }
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BookingExists(booking.Id))
                        return NotFound();
                    else
                        throw;
                }
                return RedirectToAction(nameof(Index));
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
                Text = $"{r.Type} ({r.Price} ₴)",
                Price = r.Price
            }).ToList();

            return Json(roomSelectList);
        }

        private bool BookingExists(int id)
        {
            return _context.Bookings.Any(e =>  e.Id == id);
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
