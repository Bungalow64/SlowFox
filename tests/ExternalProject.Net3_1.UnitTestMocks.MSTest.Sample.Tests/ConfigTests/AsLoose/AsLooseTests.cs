using ExternalProject.Net3_1.UnitTestMocks.MSTest.Sample.NamespaceTests;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ExternalProject.Net3_1.UnitTestMocks.MSTest.Sample.ConfigTests.AsLoose
{
    [SlowFox.InjectMocks(typeof(ReferenceDependencyViaUsing))]
    [TestClass]
    public partial class AsLooseTests
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
            var name = Create().GetName();

            Assert.AreEqual(null, name);
            _userReader.Verify(p => p.GetName(), Moq.Times.Once);
        }
    }
}