using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NServiceBus;
using NServiceBus.Logging;

namespace Billing
{
	class Program
	{
		private static ILog log = LogManager.GetLogger<Program>();
		static void Main(string[] args)
		{
			AsyncMain().GetAwaiter().GetResult();
		}

		static async Task AsyncMain()
		{
			Console.Title = "Billing";
			log.Info("Running Billing endpoint");

			EndpointConfiguration endpointConfiguration = new EndpointConfiguration("Billing");
			endpointConfiguration.UseTransport<LearningTransport>();

			var endpoint = await Endpoint.Start(endpointConfiguration).ConfigureAwait(false);

			Console.WriteLine("Press Enter to exit.");
			Console.ReadLine();

			await endpoint.Stop()
				.ConfigureAwait(false);
		}
	}
}
