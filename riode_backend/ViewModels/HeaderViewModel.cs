using riode_backend.Models;

namespace riode_backend.ViewModels
{
	public class HeaderViewModel
	{
		public Dictionary<string, string> Settings { get; set; }
		public List<BasketItem> BasketItems { get; set; }
	}
}
