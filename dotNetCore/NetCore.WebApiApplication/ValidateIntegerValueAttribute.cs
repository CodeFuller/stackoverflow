using System.ComponentModel.DataAnnotations;

namespace NetCore.WebApiApplication
{
	public class ValidateIntegerValueAttribute : ValidationAttribute
	{
		protected override ValidationResult IsValid(object value, ValidationContext validationContext)
		{
			if (value != null)
			{
				int output;

				var isInteger = int.TryParse(value.ToString(), out output);

				if (!isInteger)
				{
					return new ValidationResult("Must be a Integer number");
				}
			}

			return ValidationResult.Success;
		}
	}
}
