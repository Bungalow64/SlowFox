using NUnit.Framework;
using ExternalProject.Net5.UnitTestMocks.Sample.DependencyTypeTests;

namespace ExternalProject.Net5.UnitTestMocks.NUnit.Sample.Tests.DependencyTypeTests
{
    [TestFixture]
    [SlowFox.InjectMocks(typeof(MixedSystemTypeDependencies))]
    public partial class MixedSystemTypeDependenciesTests
    {
        [Test]
        public void Create_ObjectsExist()
        {
            MixedSystemTypeDependencies model = Create(1001, "Jamie", 10, 20, 30, 40.4);

            Assert.IsNotNull(model);
        }

        [Test]
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
