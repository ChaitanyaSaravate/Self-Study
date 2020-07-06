using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Messages;
using NServiceBus;
using NServiceBus.Logging;

namespace ClientUI
{
	class Program
	{
		private static ILog log = LogManager.GetLogger<Program>();

		public static void Main(string[] args)
		{
			AsyncMain().GetAwaiter().GetResult();
		}

		static async Task AsyncMain()
		{
			Console.Title = "ClientUI";

			var endpointConfiguration = new EndpointConfiguration("ClientUI");
			var transport = endpointConfiguration.UseTransport<LearningTransport>();
			var routing = transport.Routing();
			routing.RouteToEndpoint(typeof(PlaceOrder), "Sales");

			var endpointInstance = await Endpoint.Start(endpointConfiguration)
				.ConfigureAwait(false);

			await RunLoop(endpointInstance).ConfigureAwait(false);

			await endpointInstance.Stop()
				.ConfigureAwait(false);

			Console.ReadLine();
		}

		static async Task RunLoop(IEndpointInstance endpointInstance)
		{
			log.Info("Press P to place order and Q to quit");
			var key = Console.ReadKey();
			Console.WriteLine();

			switch (key.Key)
			{
				case ConsoleKey.P:
					var command = new PlaceOrder {OrderId = 1};
					log.Info($"PlaceOrder command sent with for the order having ID = {command.OrderId}");
					await endpointInstance.Send(command).ConfigureAwait(false);
					break;
				case ConsoleKey.Q:
					return;
				default:
					log.Info("Unknown input. Please try again.");
					break;
			}
		}
	}
}
