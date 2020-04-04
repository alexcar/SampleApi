using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace SampleApi.Requests
{
	public class CurrencyAttribute : ValidationAttribute
	{
		private readonly IList<string> _acceptedCurrencyCodes =
			new List<string>
			{
				"EUR",
				"USD",
				"GBP",
				"BRL"
			};

		protected override ValidationResult IsValid(object value, 
			ValidationContext validationContext)
		{
			return _acceptedCurrencyCodes.Any(p => p == value.ToString()) ?
				ValidationResult.Success
				: new ValidationResult($"{validationContext.MemberName} is not an accepted currency");

		}
	}
}
