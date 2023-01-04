using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Quarter.DAL;
using System.Diagnostics;
using Quarter.Models;
using Quarter.ViewModels;
using System.Linq;
using Microsoft.EntityFrameworkCore;

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
                Houses = _context.Houses.Include(x => x.Realtor).Include(x => x.Realtor).Include(x => x.Area).Include(x => x.HouseImages).Take(10).ToList(),
                Sliders = _context.Sliders.OrderBy(x => x.Order).ToList(),
                Settings = _context.Settings.ToDictionary(x => x.Key, x => x.Value),
                Tags = _context.Tags.ToList(),
                Comments = _context.Comments.Take(3).ToList(),
                AppUsers = _context.AppUsers.ToList(),
                Features = _context.Features.ToList()
            };
            return View(homeVM);
        }

       
    }
}