using ExternalProject.Net6.Constructors.Sample.NamespaceTests;

namespace ExternalProject.Net6.UnitTestMocks.NUnit.Constructors.Linked.Tests.NamespaceTests;

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
