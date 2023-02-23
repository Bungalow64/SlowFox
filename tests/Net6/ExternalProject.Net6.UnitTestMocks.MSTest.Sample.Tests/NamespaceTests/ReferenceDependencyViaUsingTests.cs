using ExternalProject.Net6.UnitTestMocks.Sample.NamespaceTests;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ExternalProject.Net6.UnitTestMocks.MSTest.Sample.NamespaceTests
{
    [SlowFox.InjectMocks(typeof(ReferenceDependencyViaUsing))]
    [TestClass]
    public partial class ReferenceDependencyViaUsingTests
    {
        [TestMethod]
        public void Create_ObjectsExist()
        {
            ReferenceDependencyViaUsing model = Create();

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