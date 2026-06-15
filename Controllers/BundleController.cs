using DataBundleSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DataBundleSystem.Controllers
{
    public class BundleController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BundleController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Customers see available bundles
        public async Task<IActionResult> Index()
        {
            var bundles = await _context.Bundles
                .Where(b => b.IsAvailable)
                .ToListAsync();
            return View(bundles);
        }

        // Admin: see all bundles
        public async Task<IActionResult> Manage()
        {
            return View(await _context.Bundles.ToListAsync());
        }

        // Admin: add new bundle page
        public IActionResult Create()
        {
            return View();
        }

        // Admin: save new bundle
        [HttpPost]
        public async Task<IActionResult> Create(Bundle bundle)
        {
            if (ModelState.IsValid)
            {
                _context.Add(bundle);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Manage));
            }
            return View(bundle);
        }
        // Admin: delete a bundle
        public async Task<IActionResult> Delete(int id)
        {
            var bundle = await _context.Bundles.FindAsync(id);
            if (bundle != null)
            {
                _context.Bundles.Remove(bundle);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Manage));
        }
    }
}
