namespace AIS_Cinema.ViewModels
{
    public class SessionDateTab
    {
        public DateTime Date { get; set; }
        public string DateStr => Date.ToString("yyyy-MM-dd");
        public string Text { get; set; } = string.Empty;
        public bool IsSelected { get; set; }
    }
}
