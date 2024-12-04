using HotelSystem.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using HotelSystem.Models;

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


    }
}
