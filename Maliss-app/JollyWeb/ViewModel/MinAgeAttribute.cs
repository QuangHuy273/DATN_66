using System.ComponentModel.DataAnnotations;

namespace JollyWeb.ViewModel
{
    public class MinAgeAttribute : ValidationAttribute
    {
        private readonly int _minAge;
        private readonly int _maxAge;

        public MinAgeAttribute(int minAge, int maxAge)
        {
            _minAge = minAge;
            _maxAge = maxAge;
        }

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value == null)
            {
                return new ValidationResult("Vui lòng chọn ngày sinh");
            }

            if (value is DateTime date)
            {
                var today = DateTime.Today;
                var age = today.Year - date.Year;

                if (date.Date > today.AddYears(-age))
                    age--;

                if (age < _minAge)
                {
                    return new ValidationResult($"Tuổi phải từ {_minAge} trở lên");
                }

                if (age > _maxAge)
                {
                    return new ValidationResult($"Tuổi không được quá {_maxAge}");
                }
            }

            return ValidationResult.Success;
        }
    }
}
