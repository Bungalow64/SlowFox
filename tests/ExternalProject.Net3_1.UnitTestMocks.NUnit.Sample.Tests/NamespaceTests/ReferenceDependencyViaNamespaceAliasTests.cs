using NUnit.Framework;
using ExternalProject.Net3_1.UnitTestMocks.Sample.NamespaceTests;

namespace ExternalProject.Net3_1.UnitTestMocks.NUnit.Sample.NamespaceTests
{
    [SlowFox.InjectMocks(typeof(ReferenceDependencyViaNamespaceAlias))]
    [TestFixture]
    public partial class ReferenceDependencyViaNamespaceAliasTets
    {
        [Test]
        public void Create_ObjectsExist()
        {
            ReferenceDependencyViaNamespaceAlias model = Create();

            Assert.That(model, Is.Not.Null);
            Assert.That(_userReader, Is.Not.Null);
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