using ExternalProject.Net7.Constructors.Sample.NamespaceTests;

namespace ExternalProject.Net7.UnitTestMocks.NUnit.Constructors.Linked.Tests.NamespaceTests;

[TestFixture]
[SlowFox.InjectMocks(typeof(ReferenceAttributeViaNamespaceAlias))]
public partial class ReferenceAttributeViaNamespaceAliasTests
{
    [Test]
    public void HasDependency()
    {
        ReferenceAttributeViaNamespaceAlias model = Create();

        Assert.That(model.Dependency, Is.EqualTo(_userReader.Object));
    }
}
