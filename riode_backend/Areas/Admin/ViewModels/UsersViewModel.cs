using riode_backend.Models;

namespace riode_backend.Areas.Admin.ViewModels
{
    public class UsersViewModel
    {
        public AppUser User { get; set; }
        public IList<string> Roles { get; set; } = null!;
    }
}
