using NUnit.Framework;
using ExternalProject.Net3_1.UnitTestMocks.Sample.NamespaceTests;
using I = SlowFox.InjectMocksAttribute;

namespace ExternalProject.Net3_1.UnitTestMocks.NUnit.Sample.AttributeNamespaceTests
{
    [I(typeof(ReferenceDependencyViaFullType))]
    [TestFixture]
    public partial class ReferenceAttributeViaTypeAliasTests
    {
        [Test]
        public void Create_ObjectsExist()
        {
            ReferenceDependencyViaFullType model = Create();

            Assert.IsNotNull(model);
            Assert.IsNotNull(_userReader);
        }

        [Test]
        public void Mock_CanMock()
        {
            _userReader.Setup(p => p.GetName()).Returns("Jamie");

            var name = Create().GetName();

            Assert.That(name, Is.EqualTo("Jamie"));
            _userReader.Verify(p => p.GetName(), Moq.Times.Once);
        }
    }
}