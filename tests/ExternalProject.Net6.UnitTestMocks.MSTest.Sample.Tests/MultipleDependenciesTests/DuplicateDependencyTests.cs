using ExternalProject.Net6.UnitTestMocks.MSTest.Sample.NamespaceTests;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ExternalProject.Net6.UnitTestMocks.MSTest.Sample.MultipleDependenciesTests.Tests
{
    [SlowFox.InjectMocks(typeof(DuplicateDependency))]
    [TestClass]
    public partial class DuplicateDependencyTests
    {
        [TestMethod]
        public void Create_ObjectsExist()
        {
            DuplicateDependency model = Create();

            Assert.IsNotNull(model);
            Assert.IsNotNull(_userReader1);
            Assert.IsNotNull(_userReader2);
            Assert.IsNotNull(_userWriter);
        }

        [TestMethod]
        public void Mock_CanMock()
        {
            _userReader1.Setup(p => p.GetName()).Returns("Jamie1");
            _userReader2.Setup(p => p.GetName()).Returns("Jamie2");

            var model = Create();

            Assert.AreEqual("Jamie1", model.GetName1());
            Assert.AreEqual("Jamie2", model.GetName2());

            _userReader1.Verify(p => p.GetName(), Moq.Times.Once);
            _userReader2.Verify(p => p.GetName(), Moq.Times.Once);
        }
    }
}