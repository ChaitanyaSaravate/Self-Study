using System;
using System.Diagnostics;
using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Receive
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Receiver Started.");

            var factory = new ConnectionFactory() { HostName = "localhost", Port = 5672 };
            using (var connection = factory.CreateConnection())
            {
                using (var channel = connection.CreateModel())
                {
                    var exchangeName = "topic_logs";

                    channel.ExchangeDeclare(exchange: exchangeName, type: ExchangeType.Topic);

                    //channel.ExchangeDeclare(exchange: exchangeName, type: ExchangeType.Direct);

                    // channel.QueueDeclare(queue: "task_queue", durable: true, exclusive: false, autoDelete: false, arguments: null);
                    // channel.BasicQos(prefetchSize: 0, prefetchCount: 1, false);

                    var queueName = channel.QueueDeclare().QueueName;
                    if (args.Length > 0)
                    {
                        foreach (var arg in args)
                        {
                            channel.QueueBind(queue: queueName, exchange: exchangeName, routingKey: arg);
                        }
                    }

                    var consumer = new EventingBasicConsumer(channel);
                    consumer.Received += (model, ea) =>
                    {
                        var body = ea.Body.ToArray();
                        var message = Encoding.UTF8.GetString(body);
                        Console.WriteLine($"Received message {message}");
                        //Process.Start(message);

                        channel.BasicAck(deliveryTag: ea.DeliveryTag, multiple: false);
                    };

                    channel.BasicConsume(queue: queueName, autoAck: false, consumer: consumer);
                    Console.ReadLine();
                }
            }

        }
    }
}
