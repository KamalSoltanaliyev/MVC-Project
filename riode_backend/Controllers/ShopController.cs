using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using riode_backend.Contexts;
using riode_backend.Models;
using riode_backend.ViewModels;
using System;
using System.Linq;

namespace riode_backend.Controllers
{
	public class ShopController : Controller
	{
		private readonly RiodeDbContext _context;
		private readonly UserManager<AppUser> _userManager;
		public ShopController(RiodeDbContext context, UserManager<AppUser> userManager)
		{
			_context = context;
			_userManager = userManager;
		}

		public async Task<IActionResult> Index()
        {
            var products = await _context.Products.Where(p => p.isStocked).Include(p => p.Category).ToListAsync();


            return View(products);
        }
        [HttpPost]
        public async Task<IActionResult> Search(SearchViewModel model)
        {
            if (model != null)
            {
                var searching = model.Searching.ToLower();
                var filteredProducts = await _context.Products.Where(p => p.Title.ToLower().Contains(searching)).Where(p => !p.isDeleted).Include(p => p.Category).ToListAsync();
                return View(filteredProducts);
            }
            else
            {
                return View(null);
            }
        }

		[HttpGet]
		public async Task<IActionResult> AddProductToBasket(int productId)
		{
			var product = await _context.Products.FirstOrDefaultAsync(p => p.Id == productId );
			if (product == null)
			{
				return NotFound();
			}
			var user = await _userManager.FindByNameAsync(User.Identity.Name);

			var basketItem = await _context.BasketItems.FirstOrDefaultAsync(b => b.ProductId == productId && b.AppUserId == user.Id);
			if (basketItem == null)
			{

				BasketItem newasketItem = new BasketItem()
				{
					ProductId = productId,
					AppUserId = user.Id,
					Count = 1,
					CreatedDate = DateTime.UtcNow,
				};

				await _context.BasketItems.AddAsync(newasketItem);
			}
			else
			{
				basketItem.Count++;
			}
			await _context.SaveChangesAsync();


			return RedirectToAction(nameof(Index));
		}
	}
}
