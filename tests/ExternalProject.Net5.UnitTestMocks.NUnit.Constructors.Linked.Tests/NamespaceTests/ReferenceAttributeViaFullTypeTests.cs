using ExternalProject.Net5.Constructors.Sample.NamespaceTests;
using NUnit.Framework;

namespace ExternalProject.Net5.UnitTestMocks.NUnit.Constructors.Linked.Tests.NamespaceTests
{
    [TestFixture]
    [SlowFox.InjectMocks(typeof(ReferenceAttributeViaFullType))]
    public partial class ReferenceAttributeViaFullTypeTests
    {
        [Test]
        public void HasDependency()
        {
            ReferenceAttributeViaFullType model = Create();

            Assert.That(model.Dependency, Is.EqualTo(_userReader.Object));
        }
    }
}