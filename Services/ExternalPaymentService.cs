using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SampleApi.Services
{
	public class ExternalPaymentService : IPaymentService
	{
		public string GetMessage() => "pay me!, I'm an external service!";
		
	}
}
