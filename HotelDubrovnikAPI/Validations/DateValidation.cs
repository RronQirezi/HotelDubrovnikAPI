using System.ComponentModel.DataAnnotations;

namespace HotelDubrovnikAPI.Validations
{
    public class DateValidation : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value is DateTime dateTime)
            {
                
                dateTime = dateTime.Date;

                
                var property = validationContext.ObjectType.GetProperty(validationContext.MemberName);
                if (property != null)
                {
                    property.SetValue(validationContext.ObjectInstance, dateTime);
                }

                return ValidationResult.Success;
            }

            return new ValidationResult("Invalid date format");
        }
    }
    //the time on db will be set as 00:00:00 and won't show up on the web side
}
