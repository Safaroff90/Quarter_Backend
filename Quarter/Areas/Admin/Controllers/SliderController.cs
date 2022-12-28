
using Microsoft.AspNetCore.Mvc;
using Quarter.DAL;
using Quarter.Helpers;
using Quarter.Models;
using Microsoft.AspNetCore.Identity;
namespace Quarter.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SliderController : Controller
    {
        private readonly QuarterContext _context;
        private readonly IWebHostEnvironment _env;

        public SliderController(QuarterContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }
        public IActionResult Index()
        {
            var model = _context.Sliders.OrderBy(x => x.Order).ToList();

            return View(model);
        }

        public IActionResult Create()
        {
            var slider = _context.Sliders.OrderByDescending(x => x.Order).FirstOrDefault();
            int order = slider == null ? 1 : slider.Order + 1;



            ViewBag.Order = order;
            return View();
        }

        [HttpPost]
        public IActionResult Create(Slider slider)
        {
            if (slider.ImageFile == null)
                ModelState.AddModelError("ImageFile", "ImageFile is required");

            if (!ModelState.IsValid)
            {
                return View();
            }

            slider.Image = FileManager.Save(slider.ImageFile, _env.WebRootPath, "uploads/sliders");


            _context.Sliders.Add(slider);
            _context.SaveChanges();

            return RedirectToAction("index");
        }

        public IActionResult Edit()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Edit(Slider slider)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            Slider existSlider = _context.Sliders.FirstOrDefault(x => x.Id == slider.Id);
            if (existSlider == null)
            {
                return RedirectToAction("Index");
            }
            existSlider.Order = slider.Order;
            existSlider.Title = slider.Title;
            existSlider.Subtitle = slider.Subtitle;
            existSlider.Desc = slider.Desc;
            existSlider.BtnText1 = slider.BtnText1;
            existSlider.BtnText2 = slider.BtnText2;
            existSlider.BtnUrl = slider.BtnUrl;
            existSlider.BtnUrl2 = slider.BtnUrl2;

            existSlider.Image = FileManager.Save(slider.ImageFile, _env.WebRootPath, "uploads/sliders");
            slider.Image = existSlider.Image;

            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        public IActionResult Delete(int id)
        {
            Slider slider = _context.Sliders.FirstOrDefault(x => x.Id == id);

            if (slider == null)
                return NotFound();

            FileManager.Delete(_env.WebRootPath, "uploads/sliders", slider.Image);

            _context.Sliders.Remove(slider);
            _context.SaveChanges();

            return Ok();
        }
    }
}
