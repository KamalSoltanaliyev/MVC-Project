using System.ComponentModel.DataAnnotations;

namespace riode_backend.ViewModels
{
    public class SearchViewModel
    {
        [Required]
        public string Searching { get; set; }
    }
}
