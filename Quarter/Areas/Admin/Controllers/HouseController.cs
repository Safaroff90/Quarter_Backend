using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Quarter.DAL;
using Quarter.Areas.Admin.ViewModels;
using Quarter.Helpers;
using Quarter.Models;
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
        public IActionResult Index(int page = 1)
        {
            var query = _context.Houses
                .Include(x => x.Category)
                .Include(x => x.Realtor)
                .Include(x => x.HouseImages);


            var model = PaginatedList<House>.Create(query, page, 4);
            return View(model);
        }
    }
}
