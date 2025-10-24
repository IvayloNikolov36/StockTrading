namespace Prices.Service;

public interface IEventProducer
{
    Task PublishEvent<T>(string queueName, string exchangeName, string routingKey, T message);
}
