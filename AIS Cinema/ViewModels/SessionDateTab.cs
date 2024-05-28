namespace AIS_Cinema.ViewModels
{
    public class SessionDateTab
    {
        public enum TabState
        {
            NotSelected,
            Selected,
            Disabled
        }

        public DateTime Date { get; set; }
        public string DateStr => Date.ToString("yyyy-MM-dd");
        public string Text { get; set; } = string.Empty;
        public TabState State { get; set; }
    }
}
