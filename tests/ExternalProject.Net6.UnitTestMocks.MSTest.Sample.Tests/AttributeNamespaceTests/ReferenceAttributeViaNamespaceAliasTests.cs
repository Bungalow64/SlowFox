using ExternalProject.Net6.UnitTestMocks.Sample.NamespaceTests;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using I = SlowFox;

namespace ExternalProject.Net6.UnitTestMocks.MSTest.Sample.AttributeNamespaceTests
{
    [I.InjectMocks(typeof(ReferenceDependencyViaFullType))]
    [TestClass]
    public partial class ReferenceAttributeViaNamespaceAliasTests
    {
        [TestMethod]
        public void Create_ObjectsExist()
        {
            ReferenceDependencyViaFullType model = Create();

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