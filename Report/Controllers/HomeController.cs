using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Report.Data;
using Report.Models;
using System.Diagnostics;
using System.Diagnostics.Metrics;

namespace Report.Controllers
{
    public class HomeController : Controller
    {
        private readonly ReportContext _context;

        public HomeController(ReportContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var rent_Buys = _context.Rent_Buy.Include(r => r.Category);
            return View(rent_Buys.ToList());
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        
    }
}