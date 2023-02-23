using ExternalProject.Net6.Constructors.Sample.ConfigTests.WithNoNullCheck;

namespace ExternalProject.Net6.UnitTestMocks.NUnit.Constructors.Linked.Tests.ConfigTests.WithNoNullCheck
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
