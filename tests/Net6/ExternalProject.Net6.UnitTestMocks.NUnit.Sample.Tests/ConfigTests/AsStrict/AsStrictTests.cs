using ExternalProject.Net6.UnitTestMocks.Sample.NamespaceTests;

namespace ExternalProject.Net6.UnitTestMocks.NUnit.Sample.ConfigTests.AsStrict
{
    [SlowFox.InjectMocks(typeof(ReferenceDependencyViaUsing))]
    [TestFixture]
    public partial class AsStrictTests
    {
        [Test]
        public void Create_ObjectsExist()
        {
            ReferenceDependencyViaUsing model = Create();

            Assert.That(model, Is.Not.Null);
            Assert.That(_userReader, Is.Not.Null);
        }

        [Test]
        public void Mock_CanMock()
        {
            TestDelegate act = () => Create().GetName();
            Assert.Throws<Moq.MockException>(act);
        }
    }
}