 
namespace XRS.EnterpriseMessaging.Response
{ 
    public interface IConsumerResponse<TKey, TContract>
    {
        TKey Key { get; }

        TContract Value { get; }
    }
}
