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

        public IActionResult Create()
        {
            ViewBag.Categories = _context.Categories.ToList();
            ViewBag.Realtors = _context.Realtors.ToList();
            ViewBag.Tags = _context.Tags.ToList();


            return View();
        }
        [HttpPost]
        public IActionResult Create(House house)
        {
            if (!_context.Realtors.Any(x => x.Id == house.RealtorId))
                ModelState.AddModelError("Realtor", "Realtor not found");

            if (!_context.Categories.Any(x => x.Id == house.CategoryId))
                ModelState.AddModelError("Category", "Category not found");

            _checkImageFiles(house.ImageFiles, house.PosterFile);

            if (!ModelState.IsValid)
            {
                ViewBag.Categories = _context.Categories.ToList();
                ViewBag.Realtors = _context.Realtors.ToList();
                ViewBag.Tags = _context.Tags.ToList();

                return View();
            }

            house.HouseImages = new List<HouseImage>();

            HouseImage poster = new HouseImage
            {
                Image = FileManager.Save(house.PosterFile, _env.WebRootPath, "uploads/houses"),
                PosterStatus = true,
            };

            house.HouseImages.Add(poster);

           


            foreach (var imgFile in house.ImageFiles)
            {
                HouseImage HouseImage = new HouseImage
                {
                    Image = FileManager.Save(imgFile, _env.WebRootPath, "uploads/houses"),
                };
                house.HouseImages.Add(HouseImage);
            }

            house.HouseTags = new List<HouseTag>();

            foreach (var tagId in house.TagIds)
            {
                if (!_context.Tags.Any(x => x.Id == tagId))
                {
                    ViewBag.Categories = _context.Categories.ToList();
                    ViewBag.Realtors = _context.Realtors.ToList();
                    ViewBag.Tags = _context.Tags.ToList();

                    ModelState.AddModelError("TagIds", "Tag not found");
                    return View();
                }

                HouseTag houseTag = new HouseTag
                {
                    TagId = tagId
                };
                house.HouseTags.Add(houseTag);
            }


            house.CreatedAt = DateTime.UtcNow.AddHours(4);
            house.UpdatedAt = DateTime.UtcNow.AddHours(4);

            _context.Houses.Add(house);
            _context.SaveChanges();

            return RedirectToAction("index");


        }
        private void _checkImageFiles(List<IFormFile> images, IFormFile posterFile)
        {
            if (posterFile == null)
                ModelState.AddModelError("PosterFile", "PosterFile is required");
            else if (posterFile.ContentType != "image/png" && posterFile.ContentType != "image/jpeg")
                ModelState.AddModelError("PosterFile", "Content type must be image/png or image/jpeg!");
            else if (posterFile != null && posterFile.Length > 2097152)
                ModelState.AddModelError("PosterFile", "File size must be less than 2MB!");

           

            if (images != null)
            {
                foreach (var imgFile in images)
                {
                    if (imgFile.ContentType != "image/png" && imgFile.ContentType != "image/jpeg")
                        ModelState.AddModelError("ImageFiles", "Content type must be image/png or image/jpeg!");

                    if (imgFile.Length > 2097152)
                        ModelState.AddModelError("ImageFiles", "File size must be less than 2MB!");
                }
            }
        }

        public IActionResult Edit(int id)
        {
            House house = _context.Houses.Include(x => x.HouseTags).Include(x => x.HouseImages).FirstOrDefault(x => x.Id == id);

            if (house == null)
                return RedirectToAction("error", "dashboard");
            ViewBag.Categories = _context.Categories.ToList();
            ViewBag.Realtors = _context.Realtors.ToList();
            ViewBag.Tags = _context.Tags.ToList();

            house.TagIds = house.HouseTags.Select(x => x.TagId).ToList();

            return View(house);
        }

        [HttpPost]
        public IActionResult Edit(House house)
        {
            House existHouse = _context.Houses.Include(x => x.HouseTags).Include(x => x.HouseImages).FirstOrDefault(x => x.Id == house.Id);

            if (existHouse == null)
                return RedirectToAction("error", "dashboard");

            if (existHouse.CategoryId != house.CategoryId && !_context.Categories.Any(x => x.Id == house.CategoryId))
                ModelState.AddModelError("CategoryId", "Category not found!");

            if (existHouse.RealtorId != house.RealtorId && !_context.Realtors.Any(x => x.Id == house.RealtorId))
                ModelState.AddModelError("AuthorId", "Author not found!");

            if (!ModelState.IsValid)
            {
                ViewBag.Categories = _context.Categories.ToList();
                ViewBag.Reators = _context.Realtors.ToList();
                ViewBag.Tags = _context.Tags.ToList();

                existHouse.TagIds = existHouse.HouseTags.Select(x => x.TagId).ToList();

                return View(existHouse);
            }

            if (house.PosterFile != null)
            {
                var poster = existHouse.HouseImages.FirstOrDefault(x => x.PosterStatus == true);
                var newPosterName = FileManager.Save(house.PosterFile, _env.WebRootPath, "uploads/houses");
                FileManager.Delete(_env.WebRootPath, "uploads/houses", poster.Image);
                poster.Image = newPosterName;
            }

            

            var removedFiles = existHouse.HouseImages.FindAll(x => x.PosterStatus == null && !house.HouseImageIds.Contains(x.Id));

            foreach (var item in removedFiles)
            {
                FileManager.Delete(_env.WebRootPath, "uploads/houses", item.Image);
            }

            //_context.HouseImages.RemoveRange(removedFiles);
            existHouse.HouseImages.RemoveAll(x => removedFiles.Contains(x));

            foreach (var imgFile in house.ImageFiles)
            {
                HouseImage houseImage = new HouseImage
                {
                    Image = FileManager.Save(imgFile, _env.WebRootPath, "uploads/houses"),
                };
                existHouse.HouseImages.Add(houseImage);
            }

            existHouse.HouseTags.RemoveAll(x => !house.TagIds.Contains(x.TagId));

            foreach (var tagId in house.TagIds.Where(x => !existHouse.HouseTags.Any(bt => bt.TagId == x)))
            {
                if (!_context.Tags.Any(x => x.Id == tagId))
                {
                    ViewBag.Categories = _context.Categories.ToList();
                    ViewBag.Realtors = _context.Realtors.ToList();
                    ViewBag.Tags = _context.Tags.ToList();

                    house.TagIds = existHouse.HouseTags.Select(x => x.TagId).ToList();

                    ModelState.AddModelError("TagIds", "Tag not found");
                    return View(existHouse);
                }

                HouseTag houseTag = new HouseTag
                {
                    TagId = tagId
                };
                existHouse.HouseTags.Add(houseTag);
            }


            existHouse.CategoryId = house.CategoryId;
            existHouse.RealtorId = house.RealtorId;
            existHouse.Title = house.Title;
            existHouse.SalePrice = house.SalePrice;
            existHouse.DiscountPercent = house.DiscountPercent;
            existHouse.CostPrice = house.CostPrice;
            existHouse.IsNew = house.IsNew;
            existHouse.IsSold = house.IsSold;
            existHouse.IsFeatured = house.IsFeatured;
            existHouse.StockStatus = house.StockStatus;

            existHouse.UpdatedAt = DateTime.UtcNow.AddHours(4);

            _context.SaveChanges();

            return RedirectToAction("index");
        }

    }
}
