using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using riode_backend.Contexts;
using riode_backend.ViewModels;
using System;

namespace riode_backend.Controllers
{
	public class ProductController : Controller
	{
		private readonly RiodeDbContext _context;

		public ProductController(RiodeDbContext context)
		{
			_context = context;
		}
		public async Task<IActionResult> ProductDetail(int id)
		{
			var products = await _context.Products.Include(p=> p.ProductImages).Where(p => p.isStocked).Include(p => p.Category).Include(p => p.Brand).FirstOrDefaultAsync(p => p.Id == id);
			if (products == null)
			{
				return NotFound();
			}

			ProductDetailViewModel model = new()
			{
				Title = products.Title,
				Description = products.Description,
				Price = products.Price,
				Rating = products.Rating,
				SKU = products.SKU,
				CategoryName = products.Category.CategoryName,
				BrandName = products.Brand.BrandName,
				Features = products.Features,
				Material = products.Material,
				ClaimedSize = products.ClaimedSize,
				RecommendedUse = products.RecommendedUse,
				Manufacturer = products.Manufacturer,
				productImage = products.PosterImage,
				
			};
			return View(model);
		}
	}
}
