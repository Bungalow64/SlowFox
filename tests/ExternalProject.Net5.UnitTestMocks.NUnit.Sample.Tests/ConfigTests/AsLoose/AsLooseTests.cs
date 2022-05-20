using NUnit.Framework;
using ExternalProject.Net5.UnitTestMocks.Sample.NamespaceTests;

namespace ExternalProject.Net5.UnitTestMocks.NUnit.Sample.ConfigTests.AsLoose
{
    [SlowFox.InjectMocks(typeof(ReferenceDependencyViaUsing))]
    [TestFixture]
    public partial class AsLooseTests
    {
        [Test]
        public void Create_ObjectsExist()
        {
            ReferenceDependencyViaUsing model = Create();

            Assert.IsNotNull(model);
            Assert.IsNotNull(_userReader);
        }

        [Test]
        public void Mock_CanMock()
        {
            var name = Create().GetName();

            Assert.AreEqual(null, name);
            _userReader.Verify(p => p.GetName(), Moq.Times.Once);
        }
    }
}