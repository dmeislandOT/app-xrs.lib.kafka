
using Confluent.Kafka;
using System.Threading;
using Google.Protobuf;

namespace XRS.Base.EnterpriseMessaging.Kafka
{
    public class KafkaTopicProducer<TKey, TValue> : IMessageServiceProducer<TKey, TValue>
    {
        public void Dispose()
        {
            if (_isDisposed)
                return;

            _isDisposed = true;
            _producer.Dispose();
        }

        public void Publish(TValue value)
        {
            Publish(default, value);
        }

        public void Publish(TKey key, TValue value)
        {
            _producer
                .Produce(
                    _topic,
                    new Message<TKey, TValue>
                    {
                        Key = key,
                        Value = value
                    });
            // Allows the Publisher to Publish....
            Thread.Sleep(1000);
        }

        public KafkaTopicProducer(string servers, string topic)
        {
            var builder = new ProducerBuilder<TKey, TValue>(new ProducerConfig
            {
                BootstrapServers = servers
            });

            if (typeof(IMessage).IsAssignableFrom(typeof(TKey)))
                builder.SetKeySerializer(new ProtoSerializer<TKey>());
            if (typeof(IMessage).IsAssignableFrom(typeof(TValue)))
                builder.SetValueSerializer(new ProtoSerializer<TValue>());

            _topic = topic;
            _producer = builder.Build();
        }

        private class ProtoSerializer<TData> : ISerializer<TData>
        {
            public byte[] Serialize(TData data, SerializationContext context)
            {
                return ((IMessage)data).ToByteArray();
            }
        }

        private bool _isDisposed;
        private readonly string _topic;

        private readonly IProducer<TKey, TValue> _producer;
    }
}
