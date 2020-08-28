using Confluent.Kafka;
using Google.Protobuf;
using System;
using Roadnet.Base.EnterpriseMessaging.Kafka;
using Roadnet.Base.EnterpriseMessaging;
using  XRS.EnterpriseMessaging.Serializers;

namespace XRS.EnterpriseMessaging.Connection
{
    public class MessaengerConnection : IMessageServiceConnection
    {

        public MessaengerConnection(string bootstrapServers)
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

        public string ServerUri { get { return _bootstrapServers; } }

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

        public IMessageServiceConsumer<TKey, TContract> GetTopicConsumer<TKey, TContract>(
        string topicName)
        {
            var type = typeof(TContract);
            if (type == typeof(string))
            {
                var  contractDes = new ContractDeserializerString();
                var stringconsumer = new KafkaContractConsumer<TKey,string,string>(_bootstrapServers, "0", topicName,contractDes);
                return stringconsumer as IMessageServiceConsumer<TKey,TContract>;
            }

            var consumer =  new KafkaContractConsumer<TKey, byte[], TContract>(
                _bootstrapServers,
                "0",
                topicName,
                new ProtobufDeserializer<TContract>());
            return consumer;
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
