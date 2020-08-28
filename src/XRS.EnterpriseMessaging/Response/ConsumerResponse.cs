

using System;

namespace XRS.EnterpriseMessaging.Response
{
    public class ConsumerResponse<TKey, TContract> : IConsumerResponse<TKey, TContract>
    {
        public TKey Key { get; set; }

        public TContract Value { get; set; }

        public ConsumerResponse() {
          
        }


    }
}
