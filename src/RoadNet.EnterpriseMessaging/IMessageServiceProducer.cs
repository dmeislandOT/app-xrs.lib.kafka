using System;

namespace Roadnet.Base.EnterpriseMessaging
{
    /// <summary>
    /// Allows for the sending of messages to a shared enterprise message bus
    /// </summary>
    public interface IMessageServiceProducer<in TValue> : IDisposable
    {
        /// <summary>
        /// Publish a message to an enterprise messaging service. A message is sent and processed
        /// as a single unit.
        /// </summary>
        /// <param name="data">An object that can be converted to a message</param>
        void Publish(TValue data);
    }

    /// <summary>
    /// Allows for the sending of messages with both a key and value to a shared enterprise message bus
    /// </summary>
    public interface IMessageServiceProducer<in TKey, in TValue> : IMessageServiceProducer<TValue>
    {
        /// <summary>
        /// Publish a message to an enterprise messaging service. A message is sent and processed
        /// as a single unit.
        /// </summary>
        /// <param name="key">A key to associated with the message</param>
        /// <param name="data">An object that can be converted to a message</param>
        void Publish(TKey key, TValue data);
    }
}
