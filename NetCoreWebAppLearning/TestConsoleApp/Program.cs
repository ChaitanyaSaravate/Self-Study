using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace TestConsoleApp
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44330/");
                Console.WriteLine("Calling Web API");
                var result = await client.GetAsync("WeatherForecast");
                Console.WriteLine("After Wen API call");
                Console.Write(result.Content.ReadAsStringAsync().Result);
            }

            Console.ReadLine();
        }
    }
}
