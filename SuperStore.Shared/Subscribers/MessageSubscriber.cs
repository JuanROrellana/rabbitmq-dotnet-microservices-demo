using System.Text;
using System.Text.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using SuperStore.Shared.Connections;

namespace SuperStore.Shared.Subscribers;

public class MessageSubscriber: IMessageSubscriber
{
    private readonly IModel _channel;
    
    public MessageSubscriber(IChannelFactory channelFactory)
        => _channel = channelFactory.Create();
    
    public IMessageSubscriber SubscribeMessage<TMessage>(string queue, string routingKey, string exchange, 
        Func<TMessage, BasicDeliverEventArgs, Task> handle) where TMessage : class, IMessage
    {
        _channel.QueueDeclare(queue, durable: false, autoDelete: false, exclusive: false);
        _channel.QueueBind(queue, exchange, routingKey);
        
        var consumer = new EventingBasicConsumer(_channel);
        consumer.Received += async (model, ea) =>
        {
            var body = ea.Body.ToArray();
            var message = JsonSerializer.Deserialize<TMessage>(Encoding.UTF8.GetString(body));

            await handle(message, ea);
        };

        _channel.BasicConsume(queue, autoAck: true, consumer: consumer);
        
        return this;
    }
}