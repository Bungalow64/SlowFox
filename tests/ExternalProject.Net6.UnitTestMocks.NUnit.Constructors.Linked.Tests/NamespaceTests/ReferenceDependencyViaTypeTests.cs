using ExternalProject.Net6.Constructors.Sample.NamespaceTests;

namespace ExternalProject.Net6.UnitTestMocks.NUnit.Constructors.Linked.Tests.NamespaceTests;

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
