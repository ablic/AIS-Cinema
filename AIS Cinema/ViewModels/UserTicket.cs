namespace AIS_Cinema.ViewModels
{
    public class UserTicket
    {
        public int Id { get; set; }
        public string SessionDateTimeStr { get; set; }
        public string MovieName { get; set; }
        public string RowAndSeatStr { get; set; }
        public decimal Price { get; set; }
        public byte[] QrCode { get; set; }
    }
}
