
using System.Threading;
using Confluent.Kafka;

namespace Roadnet.Base.EnterpriseMessaging.Kafka
{
    public sealed class KafkaContractConsumer<TKey, TWire, TContract> : IMessageServiceConsumer<TKey, TContract>
    {
        public KafkaContractConsumer(
            string bootstrapServers, 
            string groupId, 
            string topicName,
            IContractDeserializer<TWire, TContract> contractDeserializer)
        {
            var config = new ConsumerConfig
            {
                BootstrapServers = bootstrapServers,
                GroupId = groupId,
                EnableAutoCommit = true,
                EnableAutoOffsetStore = false
            };

            _consumer = new ConsumerBuilder<TKey, TWire>(config).Build();

            _consumer.Subscribe(topicName);
            _contractDeserializer = contractDeserializer;
        }

        public IConsumeResult<TKey, TContract> Consume(CancellationToken token = default)
        {
            var result = _consumer.Consume(token);
            var contract = _contractDeserializer.Deserialize(result.Message.Value);
            return new ConsumeResult(
                result.Message.Key,
                contract,
                result);
        }

        public void Commit(IConsumeResult<TKey, TContract> consumeResult)
        {
            var result = (ConsumeResult)consumeResult;
            // https://docs.confluent.io/current/clients/dotnet.html#store-offsets
            _consumer.StoreOffset(result.InternalResult);
        }

        public void Dispose()
        {
            _consumer.Dispose();
        }

        private sealed class ConsumeResult : IConsumeResult<TKey, TContract>
        {
            public ConsumeResult(TKey key, TContract value, ConsumeResult<TKey, TWire> internalResult)
            {
                Key = key;
                Value = value;
                InternalResult = internalResult;
            }

            public TKey Key { get; }
            public TContract Value { get; }
            public ConsumeResult<TKey, TWire> InternalResult { get; }
        }

        private readonly IConsumer<TKey, TWire> _consumer;
        private readonly IContractDeserializer<TWire, TContract> _contractDeserializer;
    }
}