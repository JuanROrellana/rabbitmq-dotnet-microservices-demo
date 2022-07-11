using RabbitMQ.Client.Events;
using SuperStore.Shared.Connections;

namespace SuperStore.Shared;

public interface IMessageSubscriber
{
    IMessageSubscriber SubscribeMessage<TMessage>(string queue, string routingKey, string exchange,
        Func<TMessage, BasicDeliverEventArgs, Task> handle) where TMessage : class, IMessage;
}