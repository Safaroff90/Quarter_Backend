using Microsoft.AspNetCore.Mvc;
using Quarter.DAL;

namespace Quarter.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class HouseController : Controller
    {
        private readonly QuarterContext _context;
        private readonly IWebHostEnvironment _env;

        public HouseController(QuarterContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
