
using Confluent.Kafka;
using Google.Protobuf;
using System;

namespace Roadnet.Base.EnterpriseMessaging.Kafka
{
    public class KafkaConnection : IMessageServiceConnection
    {
        public KafkaConnection(string bootstrapServers) 
        {
            _bootstrapServers = bootstrapServers;
        }

        #region Unused Code
        public void Connect()
        {
        }

        public void Dispose()
        {
        }

        public IMessageServiceConsumer GetQueueConsumer(string queueName)
        {
            throw new NotSupportedException("Kafka does not support queues");
        }

        public IMessageServiceProducer<TContract> GetQueueProducer<TContract>(string queueName)
        {
            throw new NotSupportedException("Kafka does not support queues");
        }

        public IMessageServiceConsumer GetTopicConsumer(string subscriberId, string topicName)
        {
            throw new System.NotImplementedException();
        }

        #endregion

        #region Comsumer Methods
        public IMessageServiceConsumer<TKey, TContract> GetTopicConsumer<TKey, TWire, TContract>(
            string subscriberId, 
            string topicName, 
            IContractDeserializer<TWire, TContract> contractDeserializer)
        {
            return new KafkaContractConsumer<TKey, TWire, TContract>(
                _bootstrapServers,
                subscriberId,
                topicName,
                contractDeserializer);
        }

        public IMessageServiceConsumer<TKey, TContract> GetTopicConsumer<TKey, TContract>(
            string subscriberId, 
            string topicName)
        {
            return new KafkaContractConsumer<TKey, byte[], TContract>(
                _bootstrapServers, 
                subscriberId, 
                topicName, 
                new ProtobufDeserializer<TContract>());
        }
        
        #endregion

        public IMessageServiceProducer<TValue> GetTopicProducer<TValue>(string topicName)
        {
            return GetTopicProducer<Null, TValue>(topicName);
        }

        public IMessageServiceProducer<TKey, TValue> GetTopicProducer<TKey, TValue>(string topicName)
        {
            return new KafkaTopicProducer<TKey, TValue>(_bootstrapServers, topicName);
        }

        private sealed class ProtobufDeserializer<TContract> : IContractDeserializer<byte[], TContract>
        {
            static ProtobufDeserializer()
            {
                _parser = (MessageParser)Activator.CreateInstance(
                    typeof(MessageParser<>).MakeGenericType(typeof(TContract)),
                    (Func<TContract>)(() => Activator.CreateInstance<TContract>()));
            }
            
            public TContract Deserialize(byte[] data)
            {
                return (TContract)_parser.ParseFrom(data);
            }

            private static readonly MessageParser _parser;
        }

        private readonly string _bootstrapServers;
    }
}