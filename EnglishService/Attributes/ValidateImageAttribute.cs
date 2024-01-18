using System.ComponentModel.DataAnnotations;

namespace EnglishService.Attributes
{
    public class ValidateImageAttribute: ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            // Represents the file sent with the HttpRequest
            if (value is not IFormFile file)
            {
                return false;
            }

            // Check the image mime types
            return file.ContentType.ToLower() == "image/jpg" || file.ContentType.ToLower() == "image/jpeg" || file.ContentType.ToLower() == "image/png";
        }
    }
}
