using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using riode_backend.Contexts;
using riode_backend.ViewModels;
using System;

namespace riode_backend.Controllers
{
    public class BlogController : Controller
    {
        private readonly RiodeDbContext _context;

        public BlogController(RiodeDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var blog = await _context.Blogs.ToListAsync();
            return View(blog);
        }
        public async Task<IActionResult> BlogDetail(int id)
        {
            var blog = await _context.Blogs.Where(b => !b.isDeleted).Include(b => b.BlogTopic).ThenInclude(b => b.Topic).FirstOrDefaultAsync(b => b.Id == id);

            var topics = blog.BlogTopic.Select(t => t.Topic).Select(t => t.TopicName).ToArray();

            var blogList = await _context.Blogs
            .Where(b => !b.isDeleted)
            .Include(b => b.BlogTopic)
            .ThenInclude(b => b.Topic)
            .Where(b => b.Id != id)
            .ToListAsync();

            BlogPageViewModel model = new()
            {
                Blogs = blogList,
                Blog = blog

            };

            return View(model);

        }
    }
}
