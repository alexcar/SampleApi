using SampleApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SampleApi.Repositories
{
	public class MemoryOrderRepository : IOrderRepository
	{
		private IList<Order> _orders { get; set; }

		public MemoryOrderRepository()
		{
			_orders = new List<Order>();
		}
		
		public void Add(Order order)
		{
			_orders.Add(order);
		}

		public Order Delete(Guid orderId)
		{
			var target = _orders.FirstOrDefault(p => p.Id == orderId);
			
			//_orders.Remove(target);
			target.IsInactive = true;
			Update(orderId, target);

			return target;
		}

		public IEnumerable<Order> Get()
		{
			return _orders
			.Where(p => !p.IsInactive).ToList();
		}		

		public Order Get(Guid orderid)
		{
			return _orders
				.Where(p => !p.IsInactive)
				.FirstOrDefault(p => p.Id == orderid);
		}

		public void Update(Guid orderId, Order order)
		{
			var result = _orders.FirstOrDefault(p => p.Id == orderId);
			if (result != null)
				result.ItemsIds = order.ItemsIds;
		}
	}
}
