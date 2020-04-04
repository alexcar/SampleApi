using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SampleApi.Requests
{
	public class OrderRequest
	{
		[Required]
		public IEnumerable<string> ItemsIds { get; set; }
		[Required]
		[StringLength(3)]
		[CurrencyAttribute]
		public string Currency { get; set; }
	}
}
