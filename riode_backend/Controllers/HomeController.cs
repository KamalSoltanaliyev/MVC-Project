using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using riode_backend.Contexts;
using riode_backend.ViewModels;

namespace riode_backend.Controllers
{
    public class HomeController : Controller
    {
        private readonly RiodeDbContext _context;

        public HomeController(RiodeDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var categories = await _context.Categories.Where(c => !c.isDeleted).ToListAsync();
            var products = await _context.Products.Where(p => !p.isDeleted).ToListAsync();

            HomeViewModel homeViewModel = new HomeViewModel
            {
                Categories = categories,
                Products = products
            };
            return View(homeViewModel);
        }
    }
}
