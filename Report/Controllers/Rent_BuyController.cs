using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Report.Data;
using Report.Models;

namespace Report.Controllers
{
    public class Rent_BuyController : Controller
    {
        private readonly ReportContext _context;

        public Rent_BuyController(ReportContext context)
        {
            _context = context;
        }

        // GET: Rent_Buy
        public async Task<IActionResult> Index()
        {
            var reportContext = _context.Rent_Buy.Include(r => r.Category);
            return View(await reportContext.ToListAsync());
        }
        public async Task<IActionResult> RentBuyDetails(int? id)
        {
            if (id == null || _context.Rent_Buy == null)
            {
                return NotFound();
            }

            var rent_Buy = await _context.Rent_Buy
                .Include(r => r.Category)
                .FirstOrDefaultAsync(m => m.RentBuyId == id);
            if (rent_Buy == null)
            {
                return NotFound();
            }

            return View(rent_Buy);
        }
        // GET: Rent_Buy/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Rent_Buy == null)
            {
                return NotFound();
            }

            var rent_Buy = await _context.Rent_Buy
                .Include(r => r.Category)
                .FirstOrDefaultAsync(m => m.RentBuyId == id);
            if (rent_Buy == null)
            {
                return NotFound();
            }

            return View(rent_Buy);
        }

        // GET: Rent_Buy/Create
        public IActionResult Create()
        {
            ViewData["CategoryId"] = new SelectList(_context.Category, "CategoryId", "CategoryName");
            return View();
        }

        // POST: Rent_Buy/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("RentBuyId,RentBuyName,RentBuyDescription,RentBuyProvince,RentBuyPrice,RentBuyArea,RentBuyPhone,RentBuyImage,RentBuyImage1,RentBuyImage2,RentBuyStatus,CategoryId,UserId")] Rent_Buy rent_Buy)
        {
            if (ModelState.IsValid)
            {
                _context.Add(rent_Buy);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(_context.Category, "CategoryId", "CategoryName", rent_Buy.CategoryId);
            return View(rent_Buy);
        }

        // GET: Rent_Buy/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Rent_Buy == null)
            {
                return NotFound();
            }

            var rent_Buy = await _context.Rent_Buy.FindAsync(id);
            if (rent_Buy == null)
            {
                return NotFound();
            }
            ViewData["CategoryId"] = new SelectList(_context.Category, "CategoryId", "CategoryName", rent_Buy.CategoryId);
            return View(rent_Buy);
        }

        // POST: Rent_Buy/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("RentBuyId,RentBuyName,RentBuyDescription,RentBuyProvince,RentBuyPrice,RentBuyArea,RentBuyPhone,RentBuyImage,RentBuyImage1,RentBuyImage2,RentBuyStatus,CategoryId,UserId")] Rent_Buy rent_Buy)
        {
            if (id != rent_Buy.RentBuyId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(rent_Buy);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!Rent_BuyExists(rent_Buy.RentBuyId))
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
            ViewData["CategoryId"] = new SelectList(_context.Category, "CategoryId", "CategoryName", rent_Buy.CategoryId);
            return View(rent_Buy);
        }

        // GET: Rent_Buy/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Rent_Buy == null)
            {
                return NotFound();
            }

            var rent_Buy = await _context.Rent_Buy
                .Include(r => r.Category)
                .FirstOrDefaultAsync(m => m.RentBuyId == id);
            if (rent_Buy == null)
            {
                return NotFound();
            }

            return View(rent_Buy);
        }

        // POST: Rent_Buy/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Rent_Buy == null)
            {
                return Problem("Entity set 'ReportContext.Rent_Buy'  is null.");
            }
            var rent_Buy = await _context.Rent_Buy.FindAsync(id);
            if (rent_Buy != null)
            {
                _context.Rent_Buy.Remove(rent_Buy);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool Rent_BuyExists(int id)
        {
          return (_context.Rent_Buy?.Any(e => e.RentBuyId == id)).GetValueOrDefault();
        }
    }
}
