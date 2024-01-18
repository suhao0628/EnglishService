using EnglishService.Services.IServices;
using EnglishService.Utities;
using System.Globalization;

namespace EnglishService.Services
{
    public class DateTimeParserService: IDateTimeParserService
    {
        public DateTime ConvertStrings(string date, string time)
        {
            var dateString = date + " " + time;
            const string format = GlobalConstants.DateTimeFormat;

            var dateTime = DateTime.ParseExact(dateString, format, CultureInfo.InvariantCulture);

            return dateTime;
        }
    }
}
