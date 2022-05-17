using NUnit.Framework;
using ExternalProject.Net3_1.UnitTestMocks.Sample.NamespaceTests;

namespace ExternalProject.Net3_1.UnitTestMocks.NUnit.Sample.NamespaceTests
{
    [SlowFox.InjectMocks(typeof(ReferenceDependencyViaPartialNamespaceAlias))]
    [TestFixture]
    public partial class ReferenceDependencyViaPartialNamespaceAliasTests
    {
        [Test]
        public void Create_ObjectsExist()
        {
            ReferenceDependencyViaPartialNamespaceAlias model = Create();

            Assert.IsNotNull(model);
            Assert.IsNotNull(_userReader);
        }

        [Test]
        public void Mock_CanMock()
        {
            _userReader.Setup(p => p.GetName()).Returns("Jamie");

            var name = Create().GetName();

            Assert.AreEqual("Jamie", name);
            _userReader.Verify(p => p.GetName(), Moq.Times.Once);
        }
    }
}