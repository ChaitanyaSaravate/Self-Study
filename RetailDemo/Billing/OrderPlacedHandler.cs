using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Messages;
using NServiceBus;
using NServiceBus.Logging;

namespace Sales
{
	public class OrderPlacedHandler : IHandleMessages<OrderPlaced>
	{
		private ILog log = LogManager.GetLogger<OrderPlacedHandler>();
		public Task Handle(OrderPlaced message, IMessageHandlerContext context)
		{
			log.Info($"Received OrderPlaced event for Order Id {message.OrderId}. Charging the credit card.");

			var orderBilled = new OrderBilled {OrderId = message.OrderId, BillingId = 100};
			return context.Publish(orderBilled);
		}
	}
}
