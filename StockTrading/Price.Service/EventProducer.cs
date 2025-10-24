using RabbitMQ.Client;
using System.Text;

namespace Prices.Service;

public class EventProducer : IEventProducer
{
    private readonly string messageBrokerConnectionString;

    public EventProducer(IConfiguration configuration)
    {
        this.messageBrokerConnectionString = configuration
            .GetConnectionString("RabbitMQconnection")!;
    }

    public async Task PublishEvent(
        string exchangeName, 
        string routingKey, 
        string message)
    {
        ConnectionFactory factory = new()
        {
            Uri = new Uri(this.messageBrokerConnectionString)
        };

        using IConnection connection = await factory.CreateConnectionAsync();
        using (IChannel channel = await connection.CreateChannelAsync())
        {
            await channel.ExchangeDeclareAsync(
                exchangeName, 
                ExchangeType.Topic, 
                durable: true);

            byte[] messageBody = Encoding.UTF8.GetBytes(message);

            BasicProperties props = new();

            await channel.BasicPublishAsync(
                exchangeName,
                routingKey,
                mandatory: true,
                basicProperties: props,
                body: messageBody);
        }
    }
}
