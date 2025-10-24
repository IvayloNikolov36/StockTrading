using RabbitMQ.Client;
using System.Text;
using System.Text.Json;

namespace Prices.Service;

public class EventProducer : IEventProducer
{

    private readonly string connectionString;

    public EventProducer(IConfiguration configuration)
    {
        this.connectionString = configuration
            .GetConnectionString("RabbitMQconnection")!;
    }

    public async Task PublishEvent<T>(
        string queueName,
        string exchangeName,
        string routingKey,
        T message)
    {
        ConnectionFactory factory = new()
        {
            Uri = new Uri(this.connectionString)
        };

        using IConnection connection = await factory.CreateConnectionAsync();
        using IChannel channel = await connection.CreateChannelAsync();

        await channel.ExchangeDeclareAsync(exchangeName, ExchangeType.Direct, durable: true);

        await channel.QueueDeclareAsync(
            queueName,
            durable: false,
            exclusive: false,
            autoDelete: false,
            arguments: null);

        await channel.QueueBindAsync(queueName, exchangeName, routingKey);

        string messageString = JsonSerializer.Serialize(message); 
        byte[] messageBody = Encoding.UTF8.GetBytes(messageString);

        BasicProperties props = new();

        await channel.BasicPublishAsync(
            exchangeName,
            routingKey,
            mandatory: true,
            basicProperties: props,
            body: messageBody);
    }
}
