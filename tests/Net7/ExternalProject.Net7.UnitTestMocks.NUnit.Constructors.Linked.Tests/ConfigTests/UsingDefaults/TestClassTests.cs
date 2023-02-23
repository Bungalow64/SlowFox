using ExternalProject.Net7.Constructors.Sample.ConfigTests.UsingDefaults;

namespace ExternalProject.Net7.UnitTestMocks.NUnit.Constructors.Linked.Tests.ConfigTests.UsingDefaults
{
    [TestFixture]
    [SlowFox.InjectMocks(typeof(TestClass))]
    public partial class TestClassTests
    {
        [Test]
        public void HasDependency()
        {
            TestClass model = Create();

            Assert.That(model.DataReader, Is.EqualTo(_dataReader.Object));
        }
    }
}
