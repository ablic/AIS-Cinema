using System.Text.Json.Serialization;

namespace AIS_Cinema.Models.HallLayout
{
    public class Seat
    {
        public int Number { get; set; }
        public float LeftGap { get; set; }
        public float RightGap { get; set; }
        public float PriceMultiplier { get; set; } = 1f;
    }
}
