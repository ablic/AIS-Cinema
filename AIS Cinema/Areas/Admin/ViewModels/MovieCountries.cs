using AIS_Cinema.Models;

namespace AIS_Cinema.Areas.Admin.ViewModels
{
    public class MovieCountries
    {
        public int MovieId { get; set; }
        public string MovieName { get; set; } = string.Empty;
        public List<Country> AllCountries { get; set; } = new();
        public List<int> SelectedCountryIds { get; set; } = new();
    }
}
