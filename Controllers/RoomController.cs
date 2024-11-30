using HotelSystem.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using HotelSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace HotelSystem.Controllers
{
    public class RoomController : Controller
    {
        public readonly HotelDbContext _context;

        public RoomController(HotelDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var rooms = await _context.Rooms.Include(r => r.Hotel).ToListAsync();
            return View(rooms);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Room room)
        {
            if(ModelState.IsValid)
            {
                _context.Rooms.Add(room);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(room);
        }

    }
}
