using System.Threading.Tasks;
using Messages;
using NServiceBus;
using NServiceBus.Logging;

namespace Shipping
{
	public class OrderPlacedHandler : IHandleMessages<OrderPlaced>
	{
		private ILog log = LogManager.GetLogger<OrderPlacedHandler>();
		public Task Handle(OrderPlaced message, IMessageHandlerContext context)
		{
			log.Info($"Received OrderPlaced event for Order Id {message.OrderId}. Preparing the shipment.");
			return Task.CompletedTask;
		}
	}
}
