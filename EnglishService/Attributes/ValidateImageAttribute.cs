using System.ComponentModel.DataAnnotations;

namespace EnglishService.Attributes
{
    public class ValidateImageAttribute: ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if (value is not IFormFile file)
            {
                return false;
            }

            return file.ContentType.ToLower() == "image/jpg" || file.ContentType.ToLower() == "image/jpeg" || file.ContentType.ToLower() == "image/png";
        }
    }
}
