using Roadnet.Base.EnterpriseMessaging;

namespace XRS.EnterpriseMessaging.Serializers
{
    public class ContractDeserializerString : IContractDeserializer<string, string>
    {
        public string Deserialize(string data)
        {
            return data;
        }
    }
}
