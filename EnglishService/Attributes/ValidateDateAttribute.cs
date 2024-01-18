using EnglishService.Utities;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace EnglishService.Attributes
{
    public class ValidateDateAttribute : RequiredAttribute
    {
        public override bool IsValid(object value)
        {
            var dateString = value as string;

            if (string.IsNullOrEmpty(dateString))
            {
                return false;
            }

            var parsed = DateTime.TryParseExact(
                dateString,
                GlobalConstants.DateFormat,
                CultureInfo.InvariantCulture,
                style: DateTimeStyles.AssumeUniversal,
                result: out var dt);
            if (!parsed)
            {
                return false;
            }

            return dt >= DateTime.UtcNow.Date;
        }
    }
}
