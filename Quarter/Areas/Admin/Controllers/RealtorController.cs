using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Quarter.DAL;
using Quarter.Helpers;
using Quarter.Models;
using System.Data;

namespace Quarter.Areas.Admin.Controllers
{
    [Area("manage")]
    [Authorize(Roles = "SuperAdmin,Admin")]
    public class RealtorController : Controller
    {
        private readonly QuarterContext _context;
        private readonly IWebHostEnvironment _env;

        public RealtorController(QuarterContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }
        public IActionResult Index(int page = 1)
        {
            var model = _context.Realtors.Include(x => x.House).Skip((page - 1) * 2).Take(2).ToList();
            ViewBag.Page = page;
            ViewBag.TotalPage = (int)Math.Ceiling(_context.Realtors.Count() / 2d);

            return View(model);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Realtor realtor)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            if (_context.Realtors.Any(x => x.FullName == realtor.FullName))
            {
                ModelState.AddModelError("FullName", "This Fullname has been taken");
                return View();
            }
            if (realtor.ImageFile != null && realtor.ImageFile.ContentType != "image/png" && realtor.ImageFile.ContentType != "image/jpeg")
                ModelState.AddModelError("ImageFile", "Content type must be image/png or image/jpeg!");

            if (realtor.ImageFile != null && realtor.ImageFile.Length > 2097152)
                ModelState.AddModelError("ImageFile", "File size must be less than 2MB!");
            if (!ModelState.IsValid)
            {
                return View();
            }

            realtor.Image = FileManager.Save(realtor.ImageFile, _env.WebRootPath, "uploads/sliders");

            _context.Realtors.Add(realtor);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        public IActionResult Edit(int id)
        {
            Realtor realtor = _context.Realtors.FirstOrDefault(x => x.Id == id);

            if (realtor == null)
                return RedirectToAction("error", "dashboard");


            return View(realtor);
        }



        [HttpPost]
        public IActionResult Edit(Realtor realtor)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }


            if (_context.Realtors.Any(x => x.FullName == realtor.FullName && x.Id != realtor.Id))
            {
                ModelState.AddModelError("FullName", "This Fullname has been taken");
                return View();
            }

            if (realtor.ImageFile != null && realtor.ImageFile.ContentType != "image/png" && realtor.ImageFile.ContentType != "image/jpeg")
                ModelState.AddModelError("ImageFile", "Content type must be image/png or image/jpeg!");

            if (realtor.ImageFile != null && realtor.ImageFile.Length > 2097152)
                ModelState.AddModelError("ImageFile", "File size must be less than 2MB!");

            Realtor existRealtor = _context.Realtors.FirstOrDefault(x => x.Id == realtor.Id);
            if (existRealtor == null)
            {
                return RedirectToAction("error", "dashboard");
            }

            if (realtor.ImageFile != null)
            {
                var newImageName = FileManager.Save(realtor.ImageFile, _env.WebRootPath, "uploads/sliders");

                FileManager.Delete(_env.WebRootPath, "uploads/sliders", existRealtor.Image);
                existRealtor.Image = newImageName;
            }

            existRealtor.FullName = realtor.FullName;
            existRealtor.Desc = realtor.Desc;
            _context.SaveChanges();

            return RedirectToAction("index");
        }

        public IActionResult Delete(int id)
        {
            Realtor realtor = _context.Realtors.Include(x => x.House).FirstOrDefault(x => x.Id == id);

            return View(realtor);
        }

        [HttpPost]
        public IActionResult Delete(Realtor realtor)
        {
            Realtor existRealtor = _context.Realtors.FirstOrDefault(x => x.Id == realtor.Id);

            if (!_context.Houses.Any(x => x.RealtorId == realtor.Id))
            {
                _context.Realtors.Remove(existRealtor);
                _context.SaveChanges();
            }

            return RedirectToAction("index");
        }
    }
}
