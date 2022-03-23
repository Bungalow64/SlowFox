using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ExternalProject.Net6.UnitTestMocks.MSTest.Sample.NamespaceTests.Tests
{
    [SlowFox.InjectMocks(typeof(FileScopedNamespace))]
    [TestClass]
    public partial class FileScopedNamespaceTests
    {
        [TestMethod]
        public void Create_ObjectsExist()
        {
            FileScopedNamespace model = Create();

            Assert.IsNotNull(model);
            Assert.IsNotNull(_userReader);
        }

        [TestMethod]
        public void Mock_CanMock()
        {
            _userReader.Setup(p => p.GetName()).Returns("Jamie");

            var name = Create().GetName();

            Assert.AreEqual("Jamie", name);
            _userReader.Verify(p => p.GetName(), Moq.Times.Once);
        }
    }
}