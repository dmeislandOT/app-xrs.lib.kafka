using System;
 
using Roadnet.Base.EnterpriseMessaging;

namespace XRS.EnterpriseMessaging.UnitTest.TestHelpers
{
    public class DummyServicConnection : Connection.IMessageServiceConnection
        
    {
        public bool ThrowExeption { get; set; }

        public  object Producer;
        public  object Producer2;

        public int PublishCounts { get; set; }

        public DummyServicConnection(object producer, object producer2)
        {
            Producer = producer;
            Producer2 = producer2;
        }
  
        public DummyServicConnection()
        {
            Producer = new DummyMessageProducer<string>(this);
            Producer2 = new DummyMessageProducer2<string,string>(this);
        }

        public Exception ExceptionToThrow
        {
            get; set;
        }

        public string ServerUri => "localhost:9092";

        public void InitProducers<TKey, TValue>()
        {
            Producer = new DummyMessageProducer<TValue>(this);
            Producer2 = new DummyMessageProducer2<TKey, TValue>(this);
        }
        
        
        public void Init() 
        {
            PublishCounts = 0;
            ThrowExeption = false;
            ExceptionToThrow = null;
        }

        public void SetExceptioin(Exception exception)
        {
            ThrowExeption = true;
            ExceptionToThrow = exception;
        }


        public void Connect()
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public IMessageServiceConsumer GetQueueConsumer(string queueName)
        {
            throw new NotImplementedException();
        }

        public IMessageServiceProducer<TValue> GetQueueProducer<TValue>(string queueName)
        {
            throw new NotImplementedException();
        }

        public IMessageServiceConsumer GetTopicConsumer(string subscriberId, string topicName)
        {
            throw new NotImplementedException();
        }

        public IMessageServiceConsumer<TKey, TContract> GetTopicConsumer<TKey, TWire, TContract>(string subscriberId, string topicName, IContractDeserializer<TWire, TContract> contractDeserializer)
        {
            throw new NotImplementedException();
        }

        public IMessageServiceConsumer<TKey, TContract> GetTopicConsumer<TKey, TContract>(string subscriberId, string topicName)
        {
            throw new NotImplementedException();
        }

        public IMessageServiceProducer<TValue> GetTopicProducer<TValue>(string topicName)
        {
            if (Producer != null) 
            {
                return Producer as IMessageServiceProducer<TValue>;
            }
            return new DummyMessageProducer<TValue>(this);
        }

        public IMessageServiceProducer<TKey, TValue> GetTopicProducer<TKey, TValue>(string topicName)
        {
            if (Producer2 != null)
            {
                return Producer2 as IMessageServiceProducer<TKey,TValue>;
            }
            return new DummyMessageProducer2<TKey,TValue>(this);
        }

        public IMessageServiceConsumer<TKey, TContract> GetTopicConsumer<TKey, TContract>(string topicName)
        {
            throw new NotImplementedException();
        }
    }
}
