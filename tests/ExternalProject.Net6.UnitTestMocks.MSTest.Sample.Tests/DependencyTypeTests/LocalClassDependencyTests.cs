using ExternalProject.Net6.UnitTestMocks.MSTest.Sample.DependencyTypeTests;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ExternalProject.Net6.UnitTestMocks.MSTest.Sample.Tests.DependencyTypeTests
{
    [TestClass]
    [SlowFox.InjectMocks(typeof(LocalClassDependency))]
    public partial class LocalClassDependencyTests
    {
        [TestMethod]
        public void Create_ObjectsExist()
        {
            LocalClassDependency model = Create();

            Assert.IsNotNull(model);
            Assert.IsNotNull(_localValue);
        }

        [TestMethod]
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