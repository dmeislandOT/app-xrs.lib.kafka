using System;

namespace XRS.Base.EnterpriseMessaging
{
    /// <summary>
    /// A connection to a enterprise messaging system that manages the creation of publishers
    /// and subscribers.
    /// </summary>
    public interface IMessageServiceConnection : IDisposable
    {
        /// <summary>
        /// Eagerly connect to the message service. If Connect() is not explicitly called, a connection
        /// will automatically be formed lazily on Get*Producer or Get*Consumer.
        /// </summary>
        void Connect();

        /// <summary>
        /// Get a topic publisher for the connection.
        /// </summary>
        /// <param name="topicName">A descriptor (endpoint on the bus) for the types of messages</param>
        /// <returns>A publisher to the topic</returns>
        IMessageServiceProducer<TValue> GetTopicProducer<TValue>(string topicName);

        /// <summary>
        /// Get a topic publisher for the connection.
        /// </summary>
        /// <param name="topicName">A descriptor (endpoint on the bus) for the types of messages</param>
        /// <param name="key"></param>
        /// <returns>A publisher to the topic</returns>
        IMessageServiceProducer<TKey, TValue> GetTopicProducer<TKey, TValue>(string topicName);

        /// <summary>
        /// Get a queue producer for the connection.
        /// </summary>
        /// <param name="queueName">The identifier for the queue to listen on</param>
        /// <returns>A queue identified by the input name</returns>
        IMessageServiceProducer<TValue> GetQueueProducer<TValue>(string queueName);

        /// <summary>
        /// Get a topic subscriber for the connection.
        /// </summary>
        /// <param name="subscriberId">An ID to tie to a subscriber that can resume after stopping</param>
        /// <param name="topicName">A descriptor (endpoint on the bus) for the types of messages</param>
        /// <returns>A subscriber to the topic</returns>
        IMessageServiceConsumer GetTopicConsumer(string subscriberId, string topicName);

        /// <summary>
        /// Get a topic consumer with a specific contract type. 
        /// </summary>
        /// <typeparam name="TKey">Message Key type</typeparam>
        /// <typeparam name="TWire">Message Value type over the wire</typeparam>
        /// <typeparam name="TContract">Deserialized message value type</typeparam>
        /// <param name="subscriberId">An ID to tie to a subscriber that can resume after stopping</param>
        /// <param name="topicName">A descriptor (endpoint on the bus) for the types of messages</param>
        /// <param name="contractDeserializer">Custom deserializer to parse the TWire object</param>
        /// <returns></returns>
        IMessageServiceConsumer<TKey, TContract> GetTopicConsumer<TKey, TWire, TContract>(
            string subscriberId,
            string topicName,
            IContractDeserializer<TWire, TContract> contractDeserializer);

        /// <summary>
        /// Get a topic consumer with a specific contract type.
        /// </summary>
        /// <typeparam name="TKey">Message Key type</typeparam>
        /// <typeparam name="TContract">Message Value type</typeparam>
        /// <param name="subscriberId">An ID to tie to a subscriber that can resume after stopping</param>
        /// <param name="topicName">A descriptor (endpoint on the bus) for the types of messages</param>
        /// <returns></returns>
        IMessageServiceConsumer<TKey, TContract> GetTopicConsumer<TKey, TContract>(
            string subscriberId,
            string topicName);


        IMessageServiceConsumer<TKey, TContract> GetTopicConsumer<TKey, TContract>(
        string topicName);


        /// <summary>
        /// Get a queue consumer for the connection.
        /// </summary>
        /// <param name="queueName">The identifier for the queue to listen on</param>
        /// <returns>A queue identified by the input name</returns>
        IMessageServiceConsumer GetQueueConsumer(string queueName);

        /// <summary>
        /// Boot Strap Servers used by Connection
        /// </summary>
        string ServerUri { get; }
    }
}