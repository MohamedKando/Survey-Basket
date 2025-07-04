namespace GameZone.Attributes
{
    public class MaxSizeeAttribute : ValidationAttribute
    {
        private readonly int _AllowedMaxSize;
        public MaxSizeeAttribute(int AllowedMaxSize )
        {
            _AllowedMaxSize = AllowedMaxSize;
        }
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var file = value as IFormFile;
            if (file != null)
            {
                
                var isAllowed = _AllowedMaxSize >= file.Length;
                if (!isAllowed)
                {
                    return new ValidationResult($"The Max Size is {_AllowedMaxSize} !");
                }
            }
            return ValidationResult.Success;
        }
    }
}
