using AIS_Cinema.Models;

namespace AIS_Cinema.Areas.Admin.ViewModels
{
    public class MovieGenres
    {
        public int MovieId { get; set; }
        public string MovieName { get; set; } = string.Empty;
        public List<Genre> AllGenres { get; set; } = new();
        public List<int> SelectedGenreIds { get; set; } = new();
    }
}
