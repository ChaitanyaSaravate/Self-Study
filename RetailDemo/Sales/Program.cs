using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NServiceBus;
using NServiceBus.Logging;

namespace Sales
{
	class Program
	{
		private static ILog log = LogManager.GetLogger<Program>();
		static void Main(string[] args)
		{
			AsyncMain().GetAwaiter().GetResult();

			Console.ReadLine();
		}

		static async Task AsyncMain()
		{
			Console.Title = "Sales";
			log.Info("Starting Sales endpoint");
			EndpointConfiguration endpointConfiguration = new EndpointConfiguration("Sales");
			endpointConfiguration.UseTransport<LearningTransport>();

			var endpointInstance = await Endpoint.Start(endpointConfiguration).ConfigureAwait(false);

			Console.WriteLine("Press Enter to exit.");
			Console.ReadLine();

			await endpointInstance.Stop()
				.ConfigureAwait(false);
		}
	}
}
