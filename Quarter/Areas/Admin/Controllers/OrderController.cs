using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Quarter.DAL;
using Quarter.Models;
using Quarter.Areas.Admin.ViewModels;
using System.Data;

namespace Quarter.Areas.Admin.Controllers
{
    [Area("manage")]
    [Authorize(Roles = "SuperAdmin,Admin")]
    public class OrderController : Controller
    {
        private readonly QuarterContext _context;

        public OrderController(QuarterContext context)
        {
            _context = context;
        }
        public IActionResult Index(int page = 1)
        {
            var query = _context.Orders
              .Include(x => x.OrderItems);


            var model = PaginatedList<Order>.Create(query, page, 5);
            return View(model);
        }

        public IActionResult Edit(int id)
        {
            Order order = _context.Orders.Include(x => x.OrderItems).FirstOrDefault(x => x.Id == id);

            if (order == null)
                return RedirectToAction("error", "dashboard");

            return View(order);
        }

        [HttpPost]
        public IActionResult Accept(int id)
        {
            Order order = _context.Orders.FirstOrDefault(x => x.Id == id);

            if (order == null)
                return RedirectToAction("error", "dashboard");

            order.Status = Enums.OrderStatus.Accepted;

            _context.SaveChanges();

            return RedirectToAction("index");
        }

        [HttpPost]
        public IActionResult Reject(int id)
        {
            Order order = _context.Orders.FirstOrDefault(x => x.Id == id);

            if (order == null)
                return RedirectToAction("error", "dashboard");

            order.Status = Enums.OrderStatus.Rejected;

            _context.SaveChanges();
            return RedirectToAction("index");
        }
    }
}

