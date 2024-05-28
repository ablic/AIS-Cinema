namespace AIS_Cinema.Models.HallLayout
{
    public class Row
    {
        public int Number { get; set; }
        public float FrontGap { get; set; }
        public float BackGap { get; set; }
        public List<Seat> Seats { get; set; }
    }
}
