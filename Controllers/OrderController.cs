using DataBundleSystem.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DataBundleSystem.Controllers
{
    [Authorize]
    public class OrderController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public OrderController(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // Show order form for a specific bundle
        public async Task<IActionResult> Create(int bundleId)
        {
            var bundle = await _context.Bundles.FindAsync(bundleId);
            if (bundle == null) return NotFound();

            ViewBag.Bundle = bundle;
            var order = new Order { BundleId = bundleId };
            return View(order);
        }

        // Save the order
        [HttpPost]
        public async Task<IActionResult> Create(Order order)
        {
            var bundle = await _context.Bundles.FindAsync(order.BundleId);
            if (bundle == null) return NotFound();

            order.UserId = _userManager.GetUserId(User) ?? string.Empty;
            order.AmountPaid = bundle.Price;
            order.Status = "Pending";
            order.CreatedAt = DateTime.Now;

            _context.Orders.Add(order);
            await _context.SaveChangesAsync();

            ViewBag.Bundle = bundle;
            return View("Confirmation", order);
        }

        // Admin: manage all orders
        [Authorize]
        public async Task<IActionResult> Manage()
        {
            var orders = await _context.Orders.Include(o => o.Bundle).ToListAsync();
            return View(orders);
        }

        // Admin: mark order as paid/delivered
        [Authorize]
        public async Task<IActionResult> UpdateStatus(int id, string status)
        {
            var order = await _context.Orders.FindAsync(id);
            if (order != null)
            {
                order.Status = status;
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Manage));
        }
    }
}