using ExternalProject.Net5.Constructors.Sample.NamespaceTests;
using NUnit.Framework;

namespace ExternalProject.Net5.UnitTestMocks.NUnit.Constructors.Linked.Tests.NamespaceTests
{
    [TestFixture]
    [SlowFox.InjectMocks(typeof(ReferenceDependencyViaFullType))]
    public partial class ReferenceDependencyViaFullTypeTests
    {
        [Test]
        public void HasDependency()
        {
            ReferenceDependencyViaFullType model = Create();

            Assert.AreEqual(_userReader.Object, model.Dependency);
        }
    }
}