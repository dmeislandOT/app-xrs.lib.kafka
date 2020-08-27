using System;
 
using Roadnet.Base.EnterpriseMessaging;

namespace XRS.EnterpriseMessaging.UnitTest.TestHelpers
{
    public class DummyMessageProducer<TValue> : IMessageServiceProducer<TValue>
    {

        public DummyMessageProducer(DummyServicConnection conn)
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
