using AIS_Cinema.ViewModels;

namespace AIS_Cinema
{
    public static class DateTimeUtility
    {
        private const int NumberAvailableSessionDays = 7;

        public static List<SessionDateTab> BuildSessionDateTabs(DateTime date)
        {
            List<SessionDateTab> tabs = new List<SessionDateTab>(NumberAvailableSessionDays);

            for (int i = 0; i < NumberAvailableSessionDays; i++)
            {
                SessionDateTab tab = new SessionDateTab();
                tab.Date = DateTime.Today.AddDays(i);
                tab.Text = tab.Date.FormatDateWithTodayTomorrow();
                tab.IsSelected = tab.Date == date;
                tabs.Add(tab);
            }

            return tabs;
        }

        public static string FormatDateWithTodayTomorrow(this DateTime date)
        {
            if (date.Date == DateTime.Now.Date)
            {
                return "Сегодня";
            }
            if (date.Date == DateTime.Now.AddDays(1).Date)
            {
                return "Завтра";
            }

            string dayOfMonth = GetDayOfMonth(date);
            string month = GetMonth(date);

            return $"{dayOfMonth} {month}";
        }

        public static string FormatDateTime(this DateTime dateTime)
        {
            string dayOfMonth = GetDayOfMonth(dateTime);
            string month = GetMonth(dateTime);
            string time = GetTime(dateTime);

            return $"{dayOfMonth} {month} {time}";
        }

        private static string GetDayOfMonth(DateTime dateTime)
        {
            return dateTime.Day.ToString();
        }

        private static string GetMonth(DateTime dateTime)
        {
            switch (dateTime.Month)
            {
                case 1:
                    return "января";
                case 2:
                    return "февраля";
                case 3:
                    return "марта";
                case 4:
                    return "апреля";
                case 5:
                    return "мая";
                case 6:
                    return "июня";
                case 7:
                    return "июля";
                case 8:
                    return "августа";
                case 9:
                    return "сентября";
                case 10:
                    return "октября";
                case 11:
                    return "ноября";
                case 12:
                    return "декабря";
                default:
                    return "";
            }
        }

        private static string GetTime(DateTime dateTime)
        {
            return dateTime.ToString("HH:mm");
        }
    }
}
