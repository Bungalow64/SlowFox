using ExternalProject.Net6.UnitTestMocks.MSTest.Sample.DependencyTypeTests;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ExternalProject.Net6.UnitTestMocks.MSTest.Sample.Tests.DependencyTypeTests
{
    [TestClass]
    [SlowFox.InjectMocks(typeof(MixedSystemTypeDependencies))]
    public partial class MixedSystemTypeDependenciesTests
    {
        [TestMethod]
        public void Create_ObjectsExist()
        {
            MixedSystemTypeDependencies model = Create(1001, "Jamie", 10, 20, 30, 40.4);

            Assert.IsNotNull(model);
        }

        [TestMethod]
        public void Mock_CanMock()
        {
            MixedSystemTypeDependencies model = Create(1001, "Jamie", 10, 20, 30, 40.4);

            Assert.AreEqual(1001, model.GetIndex());
            Assert.AreEqual("Jamie", model.GetName());
            Assert.AreEqual(10, model.GetLongNumber());
            Assert.AreEqual(20, model.GetInt32Number());
            Assert.AreEqual(30, model.GetInt64Number());
            Assert.AreEqual(40.4, model.GetDoubleNumber());
        }
    }
}
