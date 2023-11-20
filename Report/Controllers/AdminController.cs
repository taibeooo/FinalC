using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Report.Controllers
{
    public class AdminController : Controller
    {
        [Authorize(Roles = "Administrator")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
