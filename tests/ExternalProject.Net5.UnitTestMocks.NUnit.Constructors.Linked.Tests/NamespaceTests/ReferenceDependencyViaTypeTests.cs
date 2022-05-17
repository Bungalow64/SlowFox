using ExternalProject.Net5.Constructors.Sample.NamespaceTests;
using NUnit.Framework;

namespace ExternalProject.Net5.UnitTestMocks.NUnit.Constructors.Linked.Tests.NamespaceTests
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