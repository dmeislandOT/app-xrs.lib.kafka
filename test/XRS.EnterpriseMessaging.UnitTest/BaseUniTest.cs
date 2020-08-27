using System;
using NUnit.Framework;

namespace XRS.EnterpriseMessaging.UnitTest
{
    [TestFixture]
    public abstract class BaseUniTest<T>
    {
       public T ItemUnderTest { get; set; }

        [SetUp]
        public virtual void SetUp() 
        {
            ItemUnderTest = Activator.CreateInstance<T>();
        }
    }
}
