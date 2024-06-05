namespace AIS_Cinema.ViewModels
{
    public class MovieSessionCard
    {
        public int SessionId { get; set; }
        public string TimeStr { get; set; } = string.Empty;
        public int NumberAvailableSeats { get; set; }
        public decimal Price { get; set; }
        public int HallNumber { get; set; }
    }
}
