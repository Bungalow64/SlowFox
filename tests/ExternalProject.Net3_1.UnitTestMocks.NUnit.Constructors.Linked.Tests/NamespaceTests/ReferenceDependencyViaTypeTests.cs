using ExternalProject.Net3_1.Constructors.Sample.NamespaceTests;
using NUnit.Framework;

namespace ExternalProject.Net3_1.UnitTestMocks.NUnit.Constructors.Linked.Tests.NamespaceTests
{
    [TestFixture]
    [SlowFox.InjectMocks(typeof(ReferenceDependencyViaType))]
    public partial class ReferenceDependencyViaTypeTests
    {
        [Test]
        public void HasDependency()
        {
            ReferenceDependencyViaType model = Create();

            Assert.AreEqual(_userReader.Object, model.Dependency);
        }
    }
}