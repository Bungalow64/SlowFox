using ExternalProject.Net6.Constructors.Sample.ConfigTests.WithUnderscores;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ExternalProject.Net6.UnitTestMocks.MSTest.Constructors.Linked.Tests.ConfigTests.WithUnderscores
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
