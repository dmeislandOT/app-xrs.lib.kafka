using System;
using XRS.EnterpriseMessaging.Response;
using XRS.EnterpriseMessaging.Connection;

namespace XRS.EnterpriseMessaging
{
    public class EnterpriseMessenger : IDisposable
    {
        private readonly IMessageServiceConnection _messageServiceConnection;
      
        public EnterpriseMessenger(string bootstrapServers)
        {
            _messageServiceConnection = new MessaengerConnection(bootstrapServers);
        }
        public EnterpriseMessenger(IMessageServiceConnection connection)
        {
            _messageServiceConnection = connection;
        }

        public IConsumerResponse<TKey, TContract> Consume<TKey, TWire, TContract>(string topicName) 
        {
            var consumer = _messageServiceConnection.GetTopicConsumer<TKey,TContract>(topicName);
            var message = consumer.Consume();
            return new ConsumerResponse<TKey, TContract>
            {
                Key = message.Key,
                Value = message.Value
            };
        }
        

        public void Dispose()
        {
 
        }

        public void Publish<TValue>(string topicName, TValue data)
        {
            using (var producer = _messageServiceConnection.GetTopicProducer<TValue>(topicName))
            {
                producer.Publish(data);
            }
        }

        public void Publish<TKey, TValue>(string topicName, TValue data)
        {
           var producer = _messageServiceConnection.GetTopicProducer<TKey, TValue>(topicName);
           producer.Publish(data);
        }
    }
}
