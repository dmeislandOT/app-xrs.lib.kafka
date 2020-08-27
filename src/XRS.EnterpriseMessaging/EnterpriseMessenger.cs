using Roadnet.Base.EnterpriseMessaging.Kafka;
using Roadnet.Base.EnterpriseMessaging;
using System;

namespace XRS.EnterpriseMessaging
{
    public class EnterpriseMessenger : IDisposable
    {
        private readonly IMessageServiceConnection _messageServiceConnection;

        public EnterpriseMessenger(string bootstrapServers)
        {
            _messageServiceConnection = new KafkaConnection(bootstrapServers);
        }
        public EnterpriseMessenger(IMessageServiceConnection connection)
        {
            _messageServiceConnection = connection;
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
            using (var producer = _messageServiceConnection.GetTopicProducer<TKey, TValue>(topicName))
            {
                producer.Publish(data);
            }
        }
    }
}
