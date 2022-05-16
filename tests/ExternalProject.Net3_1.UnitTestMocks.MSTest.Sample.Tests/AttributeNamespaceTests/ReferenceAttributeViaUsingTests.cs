using ExternalProject.Net3_1.UnitTestMocks.Sample.NamespaceTests;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SlowFox;

namespace ExternalProject.Net3_1.UnitTestMocks.MSTest.Sample.AttributeNamespaceTests
{
    [InjectMocks(typeof(ReferenceDependencyViaFullType))]
    [TestClass]
    public partial class ReferenceAttributeViaUsingTests
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