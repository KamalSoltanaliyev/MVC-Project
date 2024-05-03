﻿using riode_backend.Models;
using System.ComponentModel.DataAnnotations;

namespace riode_backend.ViewModels
{
    public class ProductViewModel
    {
        public string Title { get; set; } = null!;
        public double Price { get; set; }
        public double OldPrice { get; set; }
        public double Rating { get; set; }
        public double SKU { get; set; }
        public bool isStocked { get; init; }
        public string Description { get; set; } = null!;
        public string Features { get; set; } = null!;
        public string Material { get; set; } = null!;
        public string ClaimedSize { get; set; } = null!;
        public string RecommendedUse { get; set; } = null!;
        public string Manufacturer { get; set; } = null!;

        public int BrandId { get; set; }
        public Brand? Brand { get; set; } = null!;

        public int CategoryId { get; set; }
        public Category? Category { get; set; } = null!;
        public IFormFile PosterImage { get; set; } = null!;
    }
}
