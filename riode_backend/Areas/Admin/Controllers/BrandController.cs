using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using riode_backend.Areas.Admin.ViewModels;
using riode_backend.Contexts;
using riode_backend.Helpers.Extensions;
using riode_backend.Models;
using System;

namespace riode_backend.Areas.Admin.Controllers
{
	[Area("Admin")]
	[Authorize(Roles = "Admin,Moderator")]
	public class BrandController : Controller
	{
		private readonly RiodeDbContext _context;
		private readonly IWebHostEnvironment _webHostEnvironment;
		public BrandController(RiodeDbContext context, IWebHostEnvironment webHostEnvironment)
		{
			_context = context;
			_webHostEnvironment = webHostEnvironment;
		}

		public IActionResult Index()
		{
			var brand = _context.Brands.Where(b => !b.isDeleted).ToList();

			return View(brand);
		}
		public async Task<IActionResult> Create()
		{
			if (!ModelState.IsValid)
			{
				return View();
			}
			return View();
		}
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create(BrandViewModel brand)
		{
			if (!ModelState.IsValid)
			{
				return View();
			}
			
			Brand newbrand = new()
			{
				BrandName = brand.BrandName
			};
			await _context.Brands.AddAsync(newbrand);
			await _context.SaveChangesAsync();
			return RedirectToAction("Index");
		}
		//[Authorize(Roles = "Admin")]
		public async Task<IActionResult> Delete(int id)
		{

			var brand = await _context.Brands.FirstOrDefaultAsync(b => b.Id == id && !b.isDeleted); ;
			if (brand == null)
			{
				return NotFound();
			}
			return View(brand);
		}

		[HttpPost]
		[ActionName("Delete")]
		[ValidateAntiForgeryToken]
		//[Authorize(Roles = "Admin")]
		public async Task<IActionResult> DeleteBrand(int id)
		{

			var brand = await _context.Brands.FirstOrDefaultAsync(b => b.Id == id && !b.isDeleted); ;
			if (brand == null)
			{
				return NotFound();
			}
			
			brand.isDeleted = true;
			await _context.SaveChangesAsync();
			return RedirectToAction(nameof(Index));
		}
		public async Task<IActionResult> Detail(int id)
		{

			var brand = await _context.Brands.FirstOrDefaultAsync(b => b.Id == id && !b.isDeleted);
			if (brand == null)
			{
				return NotFound();
			}
			return View(brand);
		}
		public async Task<IActionResult> Update(int id)
		{
			var brand = await _context.Brands.AsNoTracking()
				.FirstOrDefaultAsync(b => b.Id == id && !b.isDeleted);
			if (brand == null)
				return NotFound();

			BrandUpdateViewModel model = new()
			{
				BrandName = brand.BrandName,
			};

			return View(model);
		}
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Update(int id, BrandUpdateViewModel brand)
		{
			if (!ModelState.IsValid)
			{
				return View();
			}

			var updateBrand = await _context.Brands.FirstOrDefaultAsync(b => b.Id == id && !b.isDeleted);
			if (updateBrand == null)
			{
				return NotFound();
			}


			updateBrand.BrandName = brand.BrandName;

			await _context.SaveChangesAsync();

			return RedirectToAction(nameof(Index));
		}
	}
}
