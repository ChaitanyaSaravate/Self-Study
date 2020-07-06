using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Messages;
using NServiceBus;
using NServiceBus.Logging;

namespace Shipping
{
	public class OrderBilledHandler : IHandleMessages<OrderBilled>
	{
		static ILog log = LogManager.GetLogger<OrderBilledHandler>();
		public Task Handle(OrderBilled message, IMessageHandlerContext context)
		{
			log.Info($"Received OrderBilled event for Order Id {message.OrderId} and the Billing Id {message.BillingId}. Generate invoice.");
			return Task.CompletedTask;
		}
	}
}
