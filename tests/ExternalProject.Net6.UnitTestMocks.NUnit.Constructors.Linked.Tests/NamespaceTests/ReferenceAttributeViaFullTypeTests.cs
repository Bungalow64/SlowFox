using ExternalProject.Net6.Constructors.Sample.NamespaceTests;

namespace ExternalProject.Net6.UnitTestMocks.NUnit.Constructors.Linked.Tests.NamespaceTests;

[TestFixture]
[SlowFox.InjectMocks(typeof(ReferenceAttributeViaFullType))]
public partial class ReferenceAttributeViaFullTypeTests
{
    [Test]
    public void HasDependency()
    {
        ReferenceAttributeViaFullType model = Create();

        Assert.That(model.Dependency, Is.EqualTo(_userReader.Object));
    }
}
