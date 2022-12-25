using Microsoft.AspNetCore.Mvc;
using Quarter.DAL;
using System.Diagnostics;
using Quarter.Models;
using Quarter.ViewModels;
using System.Linq;
namespace Quarter.Controllers
{
    public class HomeController : Controller
    {
       private readonly QuarterContext _context;
        public HomeController(QuarterContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            HomeViewModel homeVM = new HomeViewModel
            {
                Sliders = _context.Sliders.OrderBy(x => x.Order).ToList(),
            };
            return View(homeVM);
        }

       
    }
}