using NUnit.Framework;
using XRS.EnterpriseMessaging.UnitTest.TestHelpers;

namespace XRS.EnterpriseMessaging.UnitTest
{
    [TestFixture]
    public class EnterpriseMessengerTest :BaseUniTest<EnterpriseMessenger>
    {
        private DummyServicConnection connection;
        public override void SetUp()
        {
            connection = new DummyServicConnection();
            ItemUnderTest = new EnterpriseMessenger(connection);
        }

        [Test]
        public void Publish_Should_Fire_Once()
        {
            ItemUnderTest.Publish<string>("xrs-event", "Hello World");
            Assert.AreEqual(1,connection.PublishCounts);
        }
        [Test]
        public void Publish_Should_Fire_Once2()
        {
            ItemUnderTest.Publish<string,string>("xrs-event", "Hello World");
            Assert.AreEqual(1,connection.PublishCounts);
        }

    }
}
