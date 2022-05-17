using ExternalProject.Net5.Constructors.Sample.NamespaceTests;
using NUnit.Framework;

namespace ExternalProject.Net5.UnitTestMocks.NUnit.Constructors.Linked.Tests.NamespaceTests
{
    [TestFixture]
    [SlowFox.InjectMocks(typeof(ReferenceDependencyViaRelativeType))]
    public partial class ReferenceDependencyViaRelativeTypeTests
    {
        [Test]
        public void HasDependency()
        {
            ReferenceDependencyViaRelativeType model = Create();

            Assert.AreEqual(_userReader.Object, model.Dependency);
        }
    }
}