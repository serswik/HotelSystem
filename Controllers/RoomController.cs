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
                .Select(rt => new SelectListItem 
                { 
                    Value = rt.ToString(), 
                    Text = rt.ToString() 
                })
                .ToList();

            return View(room);
        }

        // GET: Room/Edit/5
        public async Task<IActionResult> Edit (int? id)
        {
            if(id == null)
            {
                return NotFound();
            }    

            var room = await _context.Rooms.FindAsync(id);
            if(room == null)
            {
                return NotFound();
            }

            ViewBag.Hotels = await _context.Hotels.ToListAsync();
            ViewBag.RoomTypes = Enum.GetValues(typeof(RoomType))
                .Cast<RoomType>()
                .Select(rt => new SelectListItem { Value = rt.ToString(), Text = rt.ToString() })
                .ToList();

            return View(room);
        }

        // POST: Room/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,HotelId,Type,Price,Description,IsAvailable")] Room room)
        {
            if (id != room.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(room);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RoomExists(room.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Hotels = await _context.Hotels.ToListAsync();
            ViewBag.RoomTypes = Enum.GetValues(typeof(RoomType))
                .Cast<RoomType>()
                .Select(rt => new SelectListItem { Value = rt.ToString(), Text = rt.ToString() })
                .ToList();

            return View(room);
        }

        // GET: Room/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var room = await _context.Rooms
                .Include(r => r.Hotel)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (room == null)
            {
                return NotFound();
            }

            return View(room);
        }

        // GET: Room/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var room = await _context.Rooms
                .Include(r => r.Hotel)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (room == null)
            {
                return NotFound();
            }

            return View(room);
        }

        // POST: Room/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var room = await _context.Rooms.FindAsync(id);
            _context.Rooms.Remove(room);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RoomExists(int id)
        {
            return _context.Rooms.Any(e => e.Id == id);
        }
    }
}
