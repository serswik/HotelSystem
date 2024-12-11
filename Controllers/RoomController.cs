using HotelSystem.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using HotelSystem.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;

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

        public async Task<IActionResult> Create()
        {
            ViewBag.Hotels = await _context.Hotels.ToListAsync();
            ViewBag.RoomTypes = Enum.GetValues(typeof(RoomType))
                .Cast<RoomType>()
                .Select(rt => new SelectListItem { Value = rt.ToString(), Text = rt.ToString() })
                .ToList();

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Room room)
        {
            if(ModelState.IsValid)
            {
                _context.Rooms.Add(room);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Hotels = await _context.Hotels.ToListAsync();
            ViewBag.RoomTypes = Enum.GetValues(typeof(RoomType))
                .Cast<RoomType>()
                .Select(rt => new SelectListItem { Value = rt.ToString(), Text = rt.ToString() })
                .ToList();

            return View(room);
        }

    }
}
