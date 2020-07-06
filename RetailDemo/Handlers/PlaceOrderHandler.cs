using System;
using System.Threading.Tasks;
using Messages;
using NServiceBus;
using NServiceBus.Logging;

namespace Handlers
{
	class PlaceOrderHandler : IHandleMessages<PlaceOrder>
	{
		static ILog log = LogManager.GetLogger<PlaceOrderHandler>();
		static Random random = new Random();

		public Task Handle(PlaceOrder message, IMessageHandlerContext context)
		{
			log.Info($"Received command to Place Order having ID = {message.OrderId + 10}");
			//if (random.Next(0, 5) == 0)
			//{
			//	throw new Exception("Oops");
			//}
			var orderPlaced = new OrderPlaced {OrderId = message.OrderId};
			log.Info($"Order placed and the event is published for the Order having ID = {message.OrderId}");
			return context.Publish(orderPlaced);
		}
	}
}
