using ExternalProject.Net6.UnitTestMocks.Sample.NamespaceTests;

namespace ExternalProject.Net6.UnitTestMocks.NUnit.Sample.ConfigTests.NoUnderscores
{
    [SlowFox.InjectMocks(typeof(ReferenceDependencyViaUsing))]
    [TestFixture]
    public partial class NoUnderscoresTests
    {
        [Test]
        public void Create_ObjectsExist()
        {
            ReferenceDependencyViaUsing model = Create();

            Assert.IsNotNull(model);
            Assert.IsNotNull(userReader);
        }

        [Test]
        public void Mock_CanMock()
        {
            userReader.Setup(p => p.GetName()).Returns("Jamie");

            var name = Create().GetName();

            Assert.That(name, Is.EqualTo("Jamie"));
            userReader.Verify(p => p.GetName(), Moq.Times.Once);
        }
    }
}