using ExternalProject.Net3_1.Constructors.Sample.BaseClasses;
using NUnit.Framework;

namespace ExternalProject.Net3_1.UnitTestMocks.NUnit.Constructors.Linked.Tests.BaseClasses
{
    [TestFixture]
    [SlowFox.InjectMocks(typeof(DerivedBaseClassWithDependencyAliasPartialNamespace))]
    public partial class DerivedBaseClassWithDependencyAliasPartialNamespaceTests
    {
        [Test]
        public void HasDependency()
        {
            DerivedBaseClassWithDependencyAliasPartialNamespace model = Create();

            Assert.That(model.UserWriter, Is.EqualTo(_userWriter.Object));
            Assert.That(model.UserReader, Is.EqualTo(_userReader.Object));
        }
    }
}
