using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Report.Data;

namespace Report.Controllers
{
    public class BuyController : Controller
    {
        private readonly ReportContext _context;

        public BuyController(ReportContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var rent_Buys = _context.Rent_Buy.Include(r => r.Category);
            return View(rent_Buys.ToList());
        }
        [HttpGet]
        public async Task<IActionResult> Index(string searchString)
        {

            var movies = from m in _context.Rent_Buy
                         select m;

            if (!String.IsNullOrEmpty(searchString))
            {
                movies = movies.Where(s => s.RentBuyName.Contains(searchString));
            }

            return View(await movies.ToListAsync());
        }
        public async Task<IActionResult> Search(string searchString, string rentbuyprovince, int? CategoryId, int? rentbuyprice)
        {
            ViewBag.Categories = new SelectList(await _context.Category.ToListAsync(), "CategoryId", "CategoryName");

            // Fetch distinct provinces from the database
            var provinces = await _context.Rent_Buy.Select(r => r.RentBuyProvince).Distinct().ToListAsync();
            ViewBag.Rentbuy = new SelectList(provinces);

            var results = _context.Rent_Buy
                   .Where(item =>
                       (string.IsNullOrEmpty(searchString) || item.RentBuyName.Contains(searchString)) &&
                       (!CategoryId.HasValue || item.CategoryId == CategoryId)  &&
                       (string.IsNullOrEmpty(rentbuyprovince) || item.RentBuyProvince == rentbuyprovince) &&
                       (!rentbuyprice.HasValue || item.RentBuyPrice <= rentbuyprice)
                   )
                   .ToList();

            // Pass the results to the view or process them accordingly
            return View(results);
        }
    }
}
