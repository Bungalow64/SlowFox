using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ExternalProject.Net5.UnitTestMocks.MSTest.Sample.NamespaceTests
{
    [SlowFox.InjectMocks(typeof(ReferenceDependencyViaTypeAlias))]
    [TestClass]
    public partial class ReferenceDependencyViaTypeAliasTests
    {
        [TestMethod]
        public void Create_ObjectsExist()
        {
            ReferenceDependencyViaTypeAlias model = Create();

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