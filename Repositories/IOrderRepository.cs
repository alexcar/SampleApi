using SampleApi.Models;
using System;
using System.Collections.Generic;

namespace SampleApi.Repositories
{
	public interface IOrderRepository
	{
		IEnumerable<Order> Get();
		Order Get(Guid orderid);
		void Add(Order order);
		void Update(Guid orderId, Order order);
		Order Delete(Guid orderId);
	}
}
