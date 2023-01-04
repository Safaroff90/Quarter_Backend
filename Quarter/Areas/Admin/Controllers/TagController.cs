using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Quarter.DAL;
using Quarter.Models;
using System.Data;

namespace Quarter.Areas.Admin.Controllers
{
    [Area("manage")]
    [Authorize(Roles = "SuperAdmin,Admin")]
    public class TagController : Controller
    {
        private readonly QuarterContext _context;

        public TagController(QuarterContext context)
        {
            _context = context;
        }
        public IActionResult Index(int page = 1)
        {
            var model = _context.Tags.Skip((page - 1) * 2).Take(2).ToList();
            ViewBag.Page = page;
            ViewBag.TotalPage = (int)Math.Ceiling(_context.Tags.Count() / 2d);

            return View(model);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Tag tag)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            if (_context.Tags.Any(x => x.Name == tag.Name))
            {
                ModelState.AddModelError("Name", "This name has been taken");
                return View();
            }

            _context.Tags.Add(tag);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        public IActionResult Edit(int id)
        {
           Tag tag = _context.Tags.FirstOrDefault(x => x.Id == id);

            if (tag == null)
                return RedirectToAction("error", "dashboard");

            return View(tag);
        }



        [HttpPost]
        public IActionResult Edit(Tag tag)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }


            if (_context.Tags.Any(x => x.Name == tag.Name && x.Id != tag.Id))
            {
                ModelState.AddModelError("Name", "This name has been taken");
                return View();
            }

           Tag existTag = _context.Tags.FirstOrDefault(x => x.Id == tag.Id);

            existTag.Name = tag.Name;
            _context.SaveChanges();

            return RedirectToAction("index");
        }

        public IActionResult Delete(int id)
        {
           Tag tag = _context.Tags.FirstOrDefault(x => x.Id == id);

            return View(tag);
        }

        [HttpPost]
        public IActionResult Delete(Tag tag)
        {
           Tag existTag = _context.Tags.FirstOrDefault(x => x.Id == tag.Id);

            _context.Tags.Remove(existTag);
            _context.SaveChanges();

            return RedirectToAction("index");
        }
    }
}
