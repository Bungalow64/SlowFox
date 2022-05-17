using NUnit.Framework;
using ExternalProject.Net3_1.UnitTestMocks.Sample.DependencyTypeTests;

namespace ExternalProject.Net3_1.UnitTestMocks.NUnit.Sample.Tests.DependencyTypeTests
{
    [TestFixture]
    [SlowFox.InjectMocks(typeof(LocalClassDependency))]
    public partial class LocalClassDependencyTests
    {
        [Test]
        public void Create_ObjectsExist()
        {
            LocalClassDependency model = Create();

            Assert.IsNotNull(model);
            Assert.IsNotNull(_localValue);
        }

        [Test]
        public void Mock_CanMock()
        {
            _localValue
                .Setup(p => p.Name)
                .Returns("Jamie");

            LocalClassDependency model = Create();

            Assert.AreEqual("Jamie", model.GetLocalClass().Name);
        }
    }
}