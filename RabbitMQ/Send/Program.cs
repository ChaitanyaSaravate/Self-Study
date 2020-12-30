using System;
using System.Text;
using RabbitMQ.Client;

namespace Send
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Sender Started.");

            var processesToRun = new string[] { "notepad", "mspaint", "notepad", "mspaint" };//, "notepad", "mspaint", "notepad", "mspaint", "notepad", "mspaint", "notepad", "mspaint" };


            //Console.WriteLine("Name of the application you want to run?.");
            //var appNameToRun = Console.ReadLine();

            var factory = new ConnectionFactory() { HostName = "localhost", Port = 5672 };
            using (var connection = factory.CreateConnection())
            {
                using (var channel = connection.CreateModel())
                {
                    var exchangeName = "topic_logs";

                    channel.ExchangeDeclare(exchange: exchangeName, type: ExchangeType.Topic);
                    
                    //channel.ExchangeDeclare(exchange: exchangeName, type: ExchangeType.Direct);
                    
                    //channel.ExchangeDeclare(exchange: "test_exchange", type: ExchangeType.Fanout);
                    //channel.QueueDeclare(queue:"task_queue", durable:true, exclusive:false, autoDelete:false, arguments:null);

                    //for (int i = 0; i < processesToRun.Length; i++)
                    for (int i = 0; i < 30; i++)
                    {
                        string message = string.Empty;
                        if(i < 10)
                        {
                            message = "Error Logs " + i;
                        }
                        else if (i>10 && i < 20)
                        {
                            message = "Other Logs " + i;
                        }
                        else
                        {
                            message = "Message other than the Logs " + i;
                        }
                        
                        var body = Encoding.UTF8.GetBytes(message);

                        var properties = channel.CreateBasicProperties();
                        properties.Persistent = true;

                        //if (i < 10)
                        //{
                        //    channel.BasicPublish(exchange: exchangeName, routingKey: "error", basicProperties: null, body: body);
                        //}
                        //else
                        //{
                        //    channel.BasicPublish(exchange: exchangeName, routingKey: "other", basicProperties: null, body: body);
                        //}


                        if (i < 10)
                        {
                            channel.BasicPublish(exchange: exchangeName, routingKey: "logs.error.#", basicProperties: null, body: body);
                        }
                        else if (i > 10 && i < 20)
                        {
                            channel.BasicPublish(exchange: exchangeName, routingKey: "logs.other.*", basicProperties: null, body: body);
                        }
                        else
                        {
                            channel.BasicPublish(exchange: exchangeName, routingKey: "message.other.*", basicProperties: null, body: body);
                        }

                        //channel.BasicPublish(exchange:"", routingKey:"task_queue", basicProperties:properties, body:body);
                        Console.WriteLine($"Sender sent {message}");
                    }
                }
            }

            Console.ReadLine();
        }
    }
}
