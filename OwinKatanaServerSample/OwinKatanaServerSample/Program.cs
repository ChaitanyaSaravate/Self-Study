using Owin;

namespace OwinKatanaServerSample
{
	class Program
	{
		static void Main(string[] args)
		{
			string uri = "http://localhost:8081";
			//using (Microsoft.Owin.Hosting.WebApp.Start<Startup>)
		}

		public class Startup
		{
			public void Configuration(IAppBuilder appBuilder)
			{
				appBuilder.Run((executionContext) =>
				{
					return executionContext.Response.WriteAsync("Hello from the first middlewear class.");
				});
			}
		}
	}
}
