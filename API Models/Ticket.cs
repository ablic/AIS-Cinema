namespace API_Models
{
    public class Ticket
    {
        public DateTime SessionDateTime { get; set; }
        public string MovieName { get; set; }
        public int RowNumber { get; set; }
        public int SeatNumber { get; set; }
        public byte[] QrCode { get; set; }
    }
}
