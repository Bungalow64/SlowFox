using ExternalProject.Net3_1.Constructors.Sample.ConfigTests.NoUnderscores;
using NUnit.Framework;

namespace ExternalProject.Net3_1.UnitTestMocks.NUnit.Constructors.Linked.Tests.ConfigTests.NoUnderscores
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
