using ExternalProject.Net6.Constructors.Sample.NamespaceTests;

namespace ExternalProject.Net6.UnitTestMocks.NUnit.Constructors.Linked.Tests.NamespaceTests;

[TestFixture]
[SlowFox.InjectMocks(typeof(ReferenceDependencyViaFullType))]
public partial class ReferenceDependencyViaFullTypeTests
{
    [Test]
    public void HasDependency()
    {
        ReferenceDependencyViaFullType model = Create();

        Assert.That(model.Dependency, Is.EqualTo(_userReader.Object));
    }
}
