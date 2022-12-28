using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Asn1.Cmp;
using Quarter.DAL;
using Quarter.Models;
using System.Reflection.Metadata.Ecma335;

namespace Quarter.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        private readonly QuarterContext _context;

        public CategoryController(QuarterContext context)
        {
            _context = context;
        }
        public IActionResult Index(int page = 1)
        {
            var model = _context.Categories.Include(x => x.Houses).Skip((page - 1) * 2).Take(2).ToList();
            ViewBag.Page = page;
            ViewBag.TotalPage = (int)Math.Ceiling(_context.Categories.Count() / 2d);

            return View(model);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Category category)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            
            if(_context.Categories.Any(x=>x.Name == category.Name))
            {
                ModelState.AddModelError("Name","This category has already been created");
                return View();
            }

            _context.Categories.Add(category);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Edit(int id)
        {
            Category category = _context.Categories.FirstOrDefault(x => x.Id == id);



            return View(category);
        }



        [HttpPost]
        public IActionResult Edit(Category category)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }


            if (_context.Categories.Any(x => x.Name == category.Name && x.Id != category.Id))
            {
                ModelState.AddModelError("Name", "This category has already been created");
                return View();
            }

            Category existCategory = _context.Categories.FirstOrDefault(x => x.Id == category.Id);

            existCategory.Name = category.Name;
            _context.SaveChanges();

            return RedirectToAction("index");
        }

        public IActionResult Delete(int id)
        {
            Category category = _context.Categories.Include(x => x.Houses).FirstOrDefault(x => x.Id == id);

            return View(category);
        }

        [HttpPost]
        public IActionResult Delete(Category category)
        {
            Category existCategory = _context.Categories.FirstOrDefault(x => x.Id == category.Id);

            if(!_context.Houses.Any(x=> x.CategoryId == category.Id))
            {
                _context.Categories.Remove(existCategory);
                _context.SaveChanges();
            }

            return RedirectToAction("index");
        }
    }
}
