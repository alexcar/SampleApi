using System;
using System.Collections.Generic;

namespace SampleApi.Responses
{
	public class OrderResponse
	{
		public Guid Id { get; set; }
		public IEnumerable<string> ItemsIds { get; set; }
		public string Currency { get; set; }
	}
}
