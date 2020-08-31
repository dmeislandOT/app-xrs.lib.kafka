namespace XRS.Base.EnterpriseMessaging
{
    /// <summary>
    /// Defines a Key/Value consumer result
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    /// <typeparam name="TContract"></typeparam>
    public interface IConsumeResult<TKey, TContract>
    {
        TKey Key { get; }

        TContract Value { get; }
    }
}
