using EnglishService.Utities;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace EnglishService.Attributes
{
    public class ValidateTimeAttribute : RequiredAttribute
    {
        public override bool IsValid(object value)
        {
            var timeString = value as string;

            if (string.IsNullOrEmpty(timeString))
            {
                return false;
            }

            var parsed = DateTime.TryParseExact(
                timeString,
                GlobalConstants.TimeFormat,
                CultureInfo.InvariantCulture,
                style: DateTimeStyles.AssumeUniversal,
                result: out _);
            return parsed;
        }
    }
}
