namespace Portfolio.Service.Messaging.EventConsumer;

public interface IEventConsumer
{
    event OnMessageReceivedEventHandler OnMessageReceived;

    Task ConsumeMessage(
        string queueName,
        string exchangeName,
        string routingKey,
        CancellationToken stoppingToken);
}
