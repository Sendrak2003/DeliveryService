using System;
using System.ComponentModel.DataAnnotations;

namespace DeliveryService.Validation
{
    public class FutureDateAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object? value, ValidationContext validationContext)
        {
            if (value == null)
            {
                return new ValidationResult("Дата доставки не может быть пустой");
            }

            if (value is DateTime dateTime)
            {
                if (dateTime <= DateTime.Now)
                {
                    return new ValidationResult(ErrorMessage);
                }
            }

            return ValidationResult.Success!;
        }
    }
}
