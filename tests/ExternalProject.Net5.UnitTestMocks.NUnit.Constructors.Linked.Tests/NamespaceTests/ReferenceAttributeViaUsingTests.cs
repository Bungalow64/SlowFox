using ExternalProject.Net5.Constructors.Sample.NamespaceTests;
using NUnit.Framework;

namespace ExternalProject.Net5.UnitTestMocks.NUnit.Constructors.Linked.Tests.NamespaceTests
{
    [TestFixture]
    [SlowFox.InjectMocks(typeof(ReferenceAttributeViaUsing))]
    public partial class ReferenceAttributeViaUsingTests
    {
        [Test]
        public void HasDependency()
        {
            ReferenceAttributeViaUsing model = Create();

            Assert.That(model.Dependency, Is.EqualTo(_userReader.Object));
        }
    }
}