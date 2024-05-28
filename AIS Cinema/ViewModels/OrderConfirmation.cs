namespace AIS_Cinema.ViewModels
{
    public class OrderConfirmation
    {
        public string DateTimeStr { get; set; }
        public string MovieName { get; set; }
        public List<string> Seats { get; set; }
        public string Email { get; set; }
        public decimal Price { get; set; }
    }
}
