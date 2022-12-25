using Microsoft.AspNetCore.Mvc;
using Quarter.DAL;

namespace Quarter.Controllers
{
    public class HouseController : Controller
    {
        private readonly QuarterContext _context;
        public HouseController(QuarterContext context)
        {
            _context = context;
        }

        public IActionResult Detail()
        {
            return View();
        }
    }
}
