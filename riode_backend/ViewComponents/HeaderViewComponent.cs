using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using riode_backend.Contexts;
using riode_backend.Models;
using riode_backend.ViewModels;

namespace riode_backend.ViewComponents
{
	public class HeaderViewComponent : ViewComponent
	{
		private readonly RiodeDbContext _context;
		private readonly UserManager<AppUser> _userManager;

		public HeaderViewComponent(RiodeDbContext context, UserManager<AppUser> userManager)
		{
			_context = context;
			_userManager = userManager;
		}

		public async Task<IViewComponentResult> InvokeAsync()
		{
			var headerModel = new HeaderViewModel();
			var user = await _userManager.FindByNameAsync(User.Identity.Name);
			var settings = await _context.Settings.ToDictionaryAsync(x => x.Key, x => x.Value);
			var basketItems = await _context.BasketItems.Include(p => p.Product).Where(b => b.AppUserId == user.Id).ToListAsync();
			if (basketItems != null)
			{
				HeaderViewModel headerViewModel = new HeaderViewModel
				{
					Settings = settings,
					BasketItems = basketItems,

				};

				return View(headerViewModel);
			}

			return View(headerModel);

		}
	}
}
