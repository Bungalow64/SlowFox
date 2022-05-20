using ExternalProject.Net6.UnitTestMocks.Sample.DependencyTypeTests;

namespace ExternalProject.Net6.UnitTestMocks.NUnit.Sample.Tests.DependencyTypeTests
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

            Assert.That(model.GetLocalClass().Name, Is.EqualTo("Jamie"));
        }
    }
}