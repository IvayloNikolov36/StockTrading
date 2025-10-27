using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace Portfolio.Service.Messaging.EventConsumer;

public delegate void OnMessageReceivedEventHandler(EventConsumer eventConsumer, MessageReceivedEventArgs eventArgs);

public class EventConsumer : IEventConsumer
{
    private readonly string connectionString;

    public event OnMessageReceivedEventHandler OnMessageReceived;

    public EventConsumer(IConfiguration configuration)
    {
        this.connectionString = configuration
            .GetConnectionString("RabbitMqConnection")!;
    }

    public async Task ConsumeMessage(
        string queueName,
        string exchangeName,
        string routingKey,
        CancellationToken stoppingToken)
    {
        ConnectionFactory factory = new()
        {
            Uri = new Uri(this.connectionString),
            ConsumerDispatchConcurrency = 2
        };

        using IConnection connection = await factory.CreateConnectionAsync(stoppingToken);
        using IChannel channel = await connection.CreateChannelAsync(cancellationToken: stoppingToken);

        await channel.QueueDeclareAsync(
            queueName,
            durable: true,
            exclusive: false,
            autoDelete: false,
            cancellationToken: stoppingToken);

        AsyncEventingBasicConsumer consumer = new(channel);

        consumer.ReceivedAsync += Receive;

        await channel.BasicConsumeAsync(
            queueName,
            autoAck: true,
            consumer: consumer,
            cancellationToken: stoppingToken
        );
    }

    private Task Receive(object model, BasicDeliverEventArgs eventArguments)
    {
        byte[] body = eventArguments.Body.ToArray();
        string message = Encoding.UTF8.GetString(body);

        this.OnMessageReceived(this, new MessageReceivedEventArgs(message));

        return Task.CompletedTask;
    }
}
