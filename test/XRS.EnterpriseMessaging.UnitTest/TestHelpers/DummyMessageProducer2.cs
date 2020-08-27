using System;

using Roadnet.Base.EnterpriseMessaging;

namespace XRS.EnterpriseMessaging.UnitTest.TestHelpers
{
    public class DummyMessageProducer2 <TKey,TValue>: IMessageServiceProducer<TKey,TValue>
    {

        public DummyMessageProducer2(DummyServicConnection conn)
        {
            connection = conn;
        }

        private DummyServicConnection connection;

        public bool ThrowExeption { get; set; }

        public Exception ExceptionToThrow
        {
            get; set;
        }

        public void Init()
        {
            ThrowExeption = false;
            ExceptionToThrow = null;
        }

        public void SetExceptioin(Exception exception)
        {
            ThrowExeption = true;
            ExceptionToThrow = exception;
        }

        public void Dispose()
        {

        }

        public void Publish(TKey key, TValue data)
        {
              connection.PublishCounts++;
            if (ThrowExeption)
            {
                if (ExceptionToThrow == null) 
                {
                    ExceptionToThrow = new Exception("Generic Error");
                }
                throw ExceptionToThrow;
            }
        }

        public void Publish(TValue data)
        {
            connection.PublishCounts++;
            if (ThrowExeption)
            {
                if (ExceptionToThrow == null)
                {
                    ExceptionToThrow = new Exception("Generic Error");
                }
                throw ExceptionToThrow;
            }
        }
    }
}
