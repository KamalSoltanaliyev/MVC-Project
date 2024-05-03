using riode_backend.Models;
using System.ComponentModel.DataAnnotations;

namespace riode_backend.ViewModels
{
    public class BlogPageViewModel
    {
        public List<Blog> Blogs { get; set; }
        public Blog Blog { get; set; }
    }
}
