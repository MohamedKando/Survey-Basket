

namespace GameZone.Attributes
{
    public class AllowedExtentions : ValidationAttribute
    {
        private readonly string _AllowedExtentions;
        public AllowedExtentions(string AllowedExtentions)
        {
            _AllowedExtentions = AllowedExtentions;
        }
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var file = value as IFormFile;
            if (file != null)
            {
                var extention = Path.GetExtension(file.FileName);
                var isAllowed = _AllowedExtentions.Split(',').Contains(extention,StringComparer.OrdinalIgnoreCase);
                if (!isAllowed)
                {
                    return new ValidationResult($"Only {_AllowedExtentions} are allowed!");
                }
            }
            return ValidationResult.Success;
        }
    }
}
