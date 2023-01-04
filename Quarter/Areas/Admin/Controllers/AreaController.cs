using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Quarter.DAL;
using Quarter.Models;
using System.Data;

namespace Quarter.Areas.Admin.Controllers
{
    [Area("manage")]
    [Authorize(Roles = "SuperAdmin,Admin")]
    public class AreaController : Controller
    {
        private readonly QuarterContext _context;

        public AreaController(QuarterContext context)
        {
            _context = context;
        }
        public IActionResult Index(int page = 1)
        {
            var model = _context.Areas.Include(x => x.Houses).Skip((page - 1) * 2).Take(2).ToList();
            ViewBag.Page = page;
            ViewBag.TotalPage = (int)Math.Ceiling(_context.Areas.Count() / 2d);

            return View(model);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Area area)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            if (_context.Areas.Any(x => x.Name == area.Name))
            {
                ModelState.AddModelError("Name", "This name has already exist");
                return View();
            }

            _context.Areas.Add(area);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        public IActionResult Edit(int id)
        {
            Area area = _context.Areas.FirstOrDefault(x => x.Id == id);

            if (area == null)
                return RedirectToAction("error", "dashboard");

            return View(area);
        }

        [HttpPost]
        public IActionResult Edit(Area area)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }


            if (_context.Areas.Any(x => x.Name == area.Name && x.Id != area.Id))
            {
                ModelState.AddModelError("Name", "This name has already exist");
                return View();
            }

            Area existArea = _context.Areas.FirstOrDefault(x => x.Id == area.Id);

            existArea.Name = area.Name;
            _context.SaveChanges();

            return RedirectToAction("index");
        }

        public IActionResult Delete(int id)
        {
            Area area = _context.Areas.Include(x => x.Houses).FirstOrDefault(x => x.Id == id);

            return View(area);
        }

        [HttpPost]
        public IActionResult Delete(Area area)
        {
            Area existArea = _context.Areas.FirstOrDefault(x => x.Id == area.Id);

            if (!_context.Houses.Any(x => x.AreaId == area.Id))
            {
                _context.Areas.Remove(existArea);
                _context.SaveChanges();
            }

            return RedirectToAction("index");
        }
    }
}
