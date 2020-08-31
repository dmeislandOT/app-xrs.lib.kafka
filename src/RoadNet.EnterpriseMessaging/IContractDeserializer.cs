namespace XRS.Base.EnterpriseMessaging
{
    /// <summary>
    /// Defines a serializer to parse the message bus 
    /// wire object to a specific contract type.
    /// </summary>
    /// <typeparam name="TWire"></typeparam>
    /// <typeparam name="TContract"></typeparam>
    public interface IContractDeserializer<TWire, TContract>
    {
        TContract Deserialize(TWire data);
    }
}
