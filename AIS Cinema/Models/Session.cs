namespace AIS_Cinema.Models
{
    public class Session
    {
        public int Id { get; set; }
        public int MovieId { get; set; }
        public Movie? Movie { get; set; }
        public DateTime DateTime { get; set; }
        public int HallId { get; set; }
        public Hall? Hall { get; set; }
        public decimal MinPrice { get; set; }
        public List<Ticket> Tickets { get; set; } = new();
    }
}
