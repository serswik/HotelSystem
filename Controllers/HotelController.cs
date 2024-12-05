using HotelSystem.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using HotelSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace HotelSystem.Controllers
{
    public class HotelController : Controller
    {
        private readonly HotelDbContext _context;

        public HotelController(HotelDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var hotels = _context.Hotels.ToList();
            return View(hotels);
        }

        // GET: Hotel/Create
        public IActionResult Create() 
        {
            return View();
        }

        // POST: Hotels/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Address, Description")] Hotel hotel)
        {
            if (ModelState.IsValid)
            {
                
                if (string.IsNullOrEmpty(hotel.Description))
                {
                    hotel.Description = null;
                }

                _context.Add(hotel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(hotel);
        }

        // GET: Hotel/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if( id == null)
            {
                return NotFound();
            }

            var hotel = await _context.Hotels.FindAsync(id);
            if(hotel == null)
            {
                return NotFound();
            }

            return View(hotel);
        }

        // POST: Hotel/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Address,Description")] Hotel hotel)
        {
            if(id != hotel.Id)
            {
                return NotFound();
            }

            if(ModelState.IsValid)
            {
                try
                {
                    _context.Update(hotel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if(!HotelExists(hotel.Id))
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
            return View(hotel);
        }

        private bool HotelExists(int id)
        {
            return _context.Hotels.Any(e => e.Id == id);
        }

        // GET: Hotel/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if(id == null)
            {
                return NotFound();
            }

            var hotel = await _context.Hotels
                .FirstOrDefaultAsync(m => m.Id == id);
            if(hotel == null)
            {
                return NotFound();
            }

            return View(hotel);
        }

        // POST: Hotel/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var hotel = await _context.Hotels.FindAsync(id);
            if(hotel != null)
            {
                _context.Hotels.Remove(hotel);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
