using riode_backend.Models;
using System.ComponentModel.DataAnnotations;

namespace riode_backend.ViewModels
{
	public class ProductDetailViewModel
	{
		public string Title { get; set; } = null!;
		public double Price { get; set; }
        public bool isStocked { get; set; }

        public double Rating { get; set; }
		public double SKU { get; set; }

		public int BrandId { get; set; }
		public string BrandName { get; set; } = null!;

		public int CategoryId { get; set; }
		public string CategoryName { get; set; } = null!;

		public string Description { get; set; } = null!;
		public string Features { get; set; } = null!;
		public string Material { get; set; } = null!;
		public string ClaimedSize { get; set; } = null!;
		public string RecommendedUse { get; set; } = null!;
		public string Manufacturer { get; set; } = null!;
		public string productImage { get; set; }
	}
}
