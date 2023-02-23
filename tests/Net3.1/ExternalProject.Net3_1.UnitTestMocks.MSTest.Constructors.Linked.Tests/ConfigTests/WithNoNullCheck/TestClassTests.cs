using ExternalProject.Net3_1.Constructors.Sample.ConfigTests.WithNoNullCheck;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ExternalProject.Net3_1.UnitTestMocks.MSTest.Constructors.Linked.Tests.ConfigTests.WithNoNullCheck
{
    [TestClass]
    [SlowFox.InjectMocks(typeof(TestClass))]
    public partial class TestClassTests
    {
        [TestMethod]
        public void HasDependency()
        {
            TestClass model = Create();

            Assert.AreEqual(_dataReader.Object, model.DataReader);
        }
    }
}
