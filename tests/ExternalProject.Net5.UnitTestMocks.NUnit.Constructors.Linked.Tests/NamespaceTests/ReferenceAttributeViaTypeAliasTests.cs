using ExternalProject.Net5.Constructors.Sample.NamespaceTests;
using NUnit.Framework;

namespace ExternalProject.Net5.UnitTestMocks.NUnit.Constructors.Linked.Tests.NamespaceTests
{
    [TestFixture]
    [SlowFox.InjectMocks(typeof(ReferenceAttributeViaTypeAlias))]
    public partial class ReferenceAttributeViaTypeAliasTests
    {
        [Test]
        public void HasDependency()
        {
            ReferenceAttributeViaTypeAlias model = Create();

            Assert.AreEqual(_userReader.Object, model.Dependency);
        }
    }
}