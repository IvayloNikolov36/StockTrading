using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace Orders.Service;

public class EventConsumer : IEventConsumer
{
    private readonly string connectionString;

    private readonly Stack<byte[]> messages;

    public EventConsumer(IConfiguration configuration)
    {
        this.messages = new Stack<byte[]>();

        this.connectionString = configuration
            .GetConnectionString("RabbitMqConnection")!;
    }

    public async Task<string> ConsumeMessage(
        string queueName,
        string exchangeName,
        string routingKey)
    {
        ConnectionFactory factory = new()
        {
            Uri = new Uri(this.connectionString),
            // ConsumerDispatchConcurrency = 2
        };

        using IConnection connection = await factory.CreateConnectionAsync();
        using IChannel channel = await connection.CreateChannelAsync();

        await channel.QueueDeclareAsync(
            queueName,
            durable: false,
            exclusive: false,
            autoDelete: false);

        AsyncEventingBasicConsumer consumer = new(channel);

        consumer.ReceivedAsync += Receive;

        await channel
            .BasicConsumeAsync(queueName, autoAck: true, consumer: consumer);

        while (this.messages.Count == 0)
        {
            continue;
        }

        string message = Encoding.UTF8.GetString(this.messages.Pop());

        return message;
    }

    private Task Receive(object model, BasicDeliverEventArgs eventArguments)
    {
        byte[] body = eventArguments.Body.ToArray();
        this.messages.Push(body);

        return Task.CompletedTask;
    }
}
