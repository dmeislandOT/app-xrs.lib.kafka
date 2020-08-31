using System;
 

namespace XRS.EnterpriseMessaging.Connection
{
    /// <summary>
    /// A connection to a enterprise messaging system that manages the creation of publishers
    /// and subscribers.
    /// </summary>
    public interface IMessageServiceConnection : XRS.Base.EnterpriseMessaging.IMessageServiceConnection,  IDisposable
    {
    }
}