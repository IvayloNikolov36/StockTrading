namespace Orders.Service;

public interface IEventConsumer
{
    Task<string> ConsumeMessage(
        string queueName,
        string exchangeName,
        string routingKey);
}
