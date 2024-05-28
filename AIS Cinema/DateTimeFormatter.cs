namespace AIS_Cinema
{
    public static class DateTimeFormatter
    {
        public static string FormatDateTime(DateTime dateTime)
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
