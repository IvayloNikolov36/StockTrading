using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace Orders.Service.Messaging.EventConsumer;

public delegate void OnMessageReceivedEventHandler(EventConsumer eventConsumer, MessageReceivedEventArgs eventArgs);

public class EventConsumer : IEventConsumer
{
    private readonly string connectionString;

    private string? message = null;

    public EventConsumer(IConfiguration configuration)
    {
        connectionString = configuration
            .GetConnectionString("RabbitMqConnection")!;
    }

    public async Task<string> ConsumeMessage(
        string queueName,
        string exchangeName,
        string routingKey)
    {
        ConnectionFactory factory = new()
        {
            Uri = new Uri(connectionString),
        };

        using IConnection connection = await factory.CreateConnectionAsync();
        using IChannel channel = await connection.CreateChannelAsync();

        await channel.QueueDeclareAsync(
            queueName,
            durable: false,
            exclusive: false,
            autoDelete: false
        );

        AsyncEventingBasicConsumer consumer = new(channel);

        consumer.ReceivedAsync += Receive;

        await channel
            .BasicConsumeAsync(queueName, autoAck: true, consumer: consumer);

        while (this.message == null)
        {
            continue;
        }

        consumer.ReceivedAsync -= Receive;

        return this.message;
    }

    private Task Receive(object model, BasicDeliverEventArgs eventArguments)
    {
        byte[] body = eventArguments.Body.ToArray();

        this.message = Encoding.UTF8.GetString(body);

        return Task.CompletedTask;
    }
}
