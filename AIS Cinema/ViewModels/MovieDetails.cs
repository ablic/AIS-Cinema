using AIS_Cinema.Models;

namespace AIS_Cinema.ViewModels
{
    public class MovieDetails
    {
        public Movie Movie { get; set; }
        public List<SessionDateTab> DateTabs { get; set; } = new();
        public List<MovieSessionCard> SessionCards { get; set; } = new();
    }
}
