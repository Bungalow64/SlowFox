using NUnit.Framework;
using ExternalProject.Net3_1.UnitTestMocks.Sample.DependencyTypeTests;

namespace ExternalProject.Net3_1.UnitTestMocks.NUnit.Sample.Tests.DependencyTypeTests
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

            Assert.That(model.GetIndex(), Is.EqualTo(1001));
            Assert.That(model.GetName(), Is.EqualTo("Jamie"));
            Assert.That(model.GetLongNumber(), Is.EqualTo(10));
            Assert.That(model.GetInt32Number(), Is.EqualTo(20));
            Assert.That(model.GetInt64Number(), Is.EqualTo(30));
            Assert.That(model.GetDoubleNumber(), Is.EqualTo(40.4));
        }
    }
}
