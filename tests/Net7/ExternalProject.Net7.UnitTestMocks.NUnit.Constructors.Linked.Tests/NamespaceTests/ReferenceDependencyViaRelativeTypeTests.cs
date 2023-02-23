using ExternalProject.Net7.Constructors.Sample.NamespaceTests;

namespace ExternalProject.Net7.UnitTestMocks.NUnit.Constructors.Linked.Tests.NamespaceTests;

[TestFixture]
[SlowFox.InjectMocks(typeof(ReferenceDependencyViaRelativeType))]
public partial class ReferenceDependencyViaRelativeTypeTests
{
    [Test]
    public void HasDependency()
    {
        ReferenceDependencyViaRelativeType model = Create();

        Assert.That(model.Dependency, Is.EqualTo(_userReader.Object));
    }
}
