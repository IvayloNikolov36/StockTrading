namespace Orders.Service.Messaging.EventConsumer;

public interface IEventConsumer
{
    Task<string> ConsumeMessage(string queue, string exchange, string routingKey);
}
