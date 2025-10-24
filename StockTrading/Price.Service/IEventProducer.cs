namespace Prices.Service;

public interface IEventProducer
{
    Task PublishEvent(string exchangeName, string routingKey, string message);
}
