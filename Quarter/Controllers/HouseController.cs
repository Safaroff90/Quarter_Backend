//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Identity;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;
//using Newtonsoft.Json;
//using Quarter.DAL;
//using Quarter.Models;
//using Quarter.ViewModels;
//using System.Data;

//namespace Quarter.Controllers
//{
//    public class HouseController : Controller
//    {
//        private readonly QuarterContext _context;
//        private readonly UserManager<AppUser> _userManager;

//        public HouseController(QuarterContext context, UserManager<AppUser> userManager)
//        {
//            _context = context;
//            _userManager = userManager;

//        }
//        public IActionResult GetHouse(int id)
//        {
//            House house = _context.Houses.Include(x => x.Category).Include(x => x.HouseImages).FirstOrDefault(x => x.Id == id);

//            return PartialView("_HouseModalPartial", house);
//        }
//        public async Task<IActionResult> Detail(int id)
//        {
//            House house = _context.Houses
//                .Include(x => x.Category)
//                .Include(x => x.Realtor)
//                .Include(x => x.Area)
//                .Include(x => x.HouseImages)
//                .Include(x => x.Comments).ThenInclude(x => x.AppUser)
//                .Include(x => x.HouseTags).ThenInclude(x => x.Tag)
//                .FirstOrDefault(x => x.Id == id);

//            if (house == null)
//            {
//                TempData["error"] = "Ev yoxdur";
//                return RedirectToAction("index", "home");
//            }

//            HouseDetailViewModel detailVM = new HouseDetailViewModel
//            {
//                House = house,
//                CommentVM = new CommentCreateViewModel { HouseId = id },
//                RelatedHouses = _context.Houses.Include(x => x.Area)
//               .Include(x => x.Category).Include(x => x.Realtor)
//               .Include(x => x.HouseImages)
//               .Include(x => x.Comments).ThenInclude(x => x.AppUser)
//               .Include(x => x.HouseTags).ThenInclude(x => x.Tag).Where(x => x.Area == house.Area || x.Realtor == house.Realtor)
//                .Take(2).ToList(),
//                Houses = _context.Houses.Include(x => x.Area).Include(x => x.HouseImages).Take(10).ToList(),
//                Tags = _context.Tags.ToList(),
//                Categories = _context.Categories.ToList(),
//            };

//            if (User.Identity.IsAuthenticated)
//            {
//                AppUser user = await _userManager.FindByNameAsync(User.Identity.Name);

//                if (user != null)
//                {
//                    detailVM.HasComment = house.Comments.Any(x => x.AppUserId == user.Id);
//                }
//            }

//            if (house == null)
//                return NotFound();

//            return View(detailVM);
//        }

//        [Authorize(Roles = "Member")]
//        [HttpPost]
//        public async Task<IActionResult> Comment(CommentCreateViewModel commentVM)
//        {
//            AppUser user = await _userManager.FindByNameAsync(User.Identity.Name);

//            House house = await _context.Houses
//              .Include(x => x.Category)
//              .Include(x => x.Realtor)
//              .Include(x => x.HouseImages)
//              .Include(x => x.Comments).ThenInclude(x => x.AppUser)
//              .Include(x => x.HouseTags).ThenInclude(x => x.Tag)
//              .FirstOrDefaultAsync(x => x.Id == commentVM.HouseId);

//            if (house == null)
//                return NotFound();

//            if (!ModelState.IsValid)
//            {

//                HouseDetailViewModel detailVM = new HouseDetailViewModel
//                {
//                    House = house,
//                    RelatedHouses = _context.Houses.Include(x => x.HouseImages).Include(x => x.Realtor).Where(x => x.CategoryId == house.CategoryId || x.SaleManagerId == house.SaleManagerId).Take(8).ToList(),
//                    CommentVM = commentVM
//                };

//                return View("detail", detailVM);
//            }

//            Comment newComment = new Comment
//            {
//                Text = commentVM.Text,
//                AppUserId = user.Id,
//                CreatedAt = DateTime.UtcNow.AddHours(4)
//            };

//            house.Comments.Add(newComment);

//            await _context.SaveChangesAsync();
//            return RedirectToAction("detail", new { id = house.Id });
//        }

//        public async Task<IActionResult> AddToWishlist(int houseId, int count = 1)
//        {
//            AppUser user = null;


//            if (User.Identity.IsAuthenticated)
//            {
//                user = await _userManager.FindByNameAsync(User.Identity.Name);
//            }


//            if (!_context.Houses.Any(x => x.Id == houseId && x.IsSold))
//            {
//                return NotFound();
//            }
//            WishListViewModel wishList = new WishListViewModel();


//            if (user != null)
//            {
//                WishlistItem wishListItem = _context.WishlistItems.FirstOrDefault(x => x.HouseId == houseId && x.AppUserId == user.Id);

//                if (wishListItem == null)
//                {
//                    wishListItem = new WishlistItem
//                    {
//                        AppUserId = user.Id,
//                        HouseId = houseId,
//                        Count = 1

//                    };

//                    _context.WishlistItems.Add(wishListItem);
//                }
//                //else
//                //{
//                //    wishListItem.Count++;
//                //}

//                _context.SaveChanges();


//                var model = _context.WishlistItems.Include(x => x.House).ThenInclude(x => x.HouseImages).Where(x => x.AppUserId == user.Id).ToList();


//                foreach (var item in model)
//                {
//                    WishListItemViewModel itemVM = new WishListItemViewModel
//                    {
//                        House = item.House,
//                        Count = item.count,
//                        Id = item.Id
//                    };

//                    wishList.Items.Add(itemVM);
//                    wishList.TotalPrice += (item.House.SalePrice * (100 - item.House.DiscountPercent) / 100);
//                }
//            }
//            else
//            {
//                var wishListStr = HttpContext.Request.Cookies["WishList"];

//                List<WishListItemCookieViewModel> wishListCookieItems = null;
//                if (wishListStr == null)
//                {
//                    wishListCookieItems = new List<WishListItemCookieViewModel>();
//                }
//                else
//                {
//                    wishListCookieItems = JsonConvert.DeserializeObject<List<WishListItemCookieViewModel>>(wishListStr);
//                }


//                WishListItemCookieViewModel wishListCookieItem = wishListCookieItems.FirstOrDefault(x => x.HouseId == houseId);

//                if (wishListCookieItem == null)
//                {
//                    wishListCookieItem = new WishListItemCookieViewModel
//                    {
//                        HouseId = houseId,
//                        Count = 1
//                    };

//                    wishListCookieItems.Add(wishListCookieItem);
//                }
//                //else
//                //{
//                //    wishListCookieItem.Count++;
//                //}


//                var jsonStr = JsonConvert.SerializeObject(wishListCookieItems);
//                HttpContext.Response.Cookies.Append("wishList", jsonStr);



//                foreach (var item in wishListCookieItems)
//                {
//                    House house = _context.Houses.Include(x => x.HouseImages).FirstOrDefault(x => x.Id == item.HouseId);

//                    WishListItemViewModel itemVM = new WishListItemViewModel
//                    {
//                        House = house,
//                        Count = item.Count,
//                        Id = 0
//                    };

//                    wishList.Items.Add(itemVM);
//                    wishList.TotalPrice += (itemVM.House.SalePrice * (100 - itemVM.House.DiscountPercent) / 100);
//                }
//            }
//            return PartialView("_WishListPartial", wishList);
//        }

//        public IActionResult GetWishList()
//        {
//            var wishListStr = HttpContext.Request.Cookies["wishList"];

//            var wishList = JsonConvert.DeserializeObject<List<WishListItemCookieViewModel>>(wishListStr);

//            return Ok(wishList);
//        }
//    }
//}
