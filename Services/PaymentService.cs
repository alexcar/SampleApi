using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SampleApi.Services
{
	public class PaymentService : IPaymentService
	{
		public string GetMessage() => "Pay me!";
		
	}
}
