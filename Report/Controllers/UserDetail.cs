using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Report.Data;
using Report.Models;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Report.Controllers
{
    public class UserDetail : Controller
    {
        private readonly ReportContext _context;

        public UserDetail(ReportContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            return _context.User != null ?
                        View(await _context.User.ToListAsync()) :
                        Problem("Entity set 'ReportContext.User'  is null.");
        }
        public IActionResult Create()
        {
            ViewData["CategoryId"] = new SelectList(_context.Category, "CategoryId", "CategoryName");
            return View();
        }

       
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


        public async Task<IActionResult> AllPost()
        {
            var reportContext = _context.Rent_Buy.Include(r => r.Category);
            return View(await reportContext.ToListAsync());
        }
        public async Task<IActionResult> EditPost(int? id)
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditPost(int id, [Bind("RentBuyId,RentBuyName,RentBuyDescription,RentBuyProvince,RentBuyPrice,RentBuyArea,RentBuyPhone,RentBuyImage,RentBuyImage1,RentBuyImage2,RentBuyStatus,CategoryId,UserId")] Rent_Buy rent_Buy)
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

       
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
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
        private string HashPassword(string password)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = Encoding.UTF8.GetBytes(password);
                byte[] hash = sha256.ComputeHash(bytes);

                // Chuyển giá trị băm thành một chuỗi hex
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < hash.Length; i++)
                {
                    sb.Append(hash[i].ToString("x2"));
                }

                // Cắt chuỗi kết quả thành 40 ký tự
                return sb.ToString().Substring(0, 40);
            }
        }
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.User == null)
            {
                return NotFound();
            }

            var user = await _context.User.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("UserId,UserName,UserPassword,UserEmail,UserRole")] User user)
        {
            if (id != user.UserId)
            {
                return NotFound();
            }
            
            if (ModelState.IsValid)
            {
                try
                {
                    user.UserPassword = HashPassword(user.UserPassword);
                    _context.Update(user);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserExists(user.UserId))
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
            return View(user);
        }
        private bool UserExists(int id)
        {
            return (_context.User?.Any(e => e.UserId == id)).GetValueOrDefault();
        }
        

    }
}
