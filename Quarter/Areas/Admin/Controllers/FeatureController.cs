using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Quarter.DAL;
using Quarter.Models;
using System.Data;

namespace Quarter.Areas.Admin.Controllers
{
    [Area("manage")]
    [Authorize(Roles = "SuperAdmin,Admin")]
    public class FeatureController : Controller
    {
        private readonly QuarterContext _context;

        public FeatureController(QuarterContext context)
        {
            _context = context;
        }
        public IActionResult Index(int page = 1)
        {
            var model = _context.Features.Skip((page - 1) * 2).Take(2).ToList();
            ViewBag.Page = page;
            ViewBag.TotalPage = (int)Math.Ceiling(_context.Features.Count() / 2d);

            return View(model);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Feature feature)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            if (_context.Features.Any(x => x.Title == feature.Title))
            {
                ModelState.AddModelError("Title", "This title has already exist");
                return View();
            }

            _context.Features.Add(feature);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        public IActionResult Edit(int id)
        {
            Feature feature = _context.Features.FirstOrDefault(x => x.Id == id);

            if (feature == null)
                return RedirectToAction("error", "dashboard");

            return View(feature);
        }



        [HttpPost]
        public IActionResult Edit(Feature feature)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }


            if (_context.Features.Any(x => x.Title == feature.Title && x.Id != feature.Id))
            {
                ModelState.AddModelError("Title", "This title has already exist");
                return View();
            }

            Feature existFeature = _context.Features.FirstOrDefault(x => x.Id == feature.Id);

            existFeature.Title = feature.Title;
            _context.SaveChanges();

            return RedirectToAction("index");
        }

        public IActionResult Delete(int id)
        {
            Feature feature = _context.Features.FirstOrDefault(x => x.Id == id);

            return View(feature);
        }

        [HttpPost]
        public IActionResult Delete(Feature feature)
        {
            Feature existFeature = _context.Features.FirstOrDefault(x => x.Id == feature.Id);

            _context.Features.Remove(existFeature);
            _context.SaveChanges();

            return RedirectToAction("index");
        }
    }
}
