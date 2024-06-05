namespace AIS_Cinema.ViewModels
{
    public class SessionCard
    {
        public int SessionId { get; set; }
        public string MovieName { get; set; } = string.Empty;
        public string DateTimeStr { get; set; } = string.Empty;
        public List<string> MovieGenreNames { get; set; } = new();
        public decimal Price { get; set; }
        public int NumberAvailableSeats { get; set; }
        public int HallNumber { get; set; }
    }
}
