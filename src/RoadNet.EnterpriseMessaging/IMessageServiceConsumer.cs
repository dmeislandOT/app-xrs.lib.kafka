using System;
using System.Threading;

namespace XRS.Base.EnterpriseMessaging
{
    /// <summary>
    /// Allows for the receiving of messages from a shared enterprise message bus
    /// </summary>
    public interface IMessageServiceConsumer : IDisposable
    {
        /// <summary>
        /// Process a single message off the enterprise messaging bus. Take the message off the bus
        /// and commit if successful else put the message back on the bus for further processing.
        /// The method will block the current thread until a message is received.
        /// </summary>
        /// <param name="messageHandlerFunc">A function that will handle a single message. The first
        /// is the topic we've received a message from. The second parameter is the content of the message
        /// as a string. Return true if the message was processed successfully and can be taken off the
        /// message bus.</param>
        void ProcessNextMessage(Func<string, string, bool> messageHandlerFunc);
    }

    /// <summary>
    /// Defines a message consumer .
    /// </summary>
    /// <typeparam name="TKey">Message key</typeparam>
    /// <typeparam name="TContract">Message contract</typeparam>
    public interface IMessageServiceConsumer<TKey, TContract> : IDisposable
    {
        /// <summary>
        /// Consume the next available message.
        /// </summary>
        /// <returns></returns>
        IConsumeResult<TKey, TContract> Consume(CancellationToken token = default);

        /// <summary>
        /// Notify the consumer you are finished with the message.
        /// </summary>
        /// <param name="consumeResult"></param>
        void Commit(IConsumeResult<TKey, TContract> consumeResult);
    }
}
