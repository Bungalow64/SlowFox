using ExternalProject.Net6.Constructors.Sample.NamespaceTests;

namespace ExternalProject.Net6.UnitTestMocks.NUnit.Constructors.Linked.Tests.NamespaceTests;

[TestFixture]
[SlowFox.InjectMocks(typeof(ReferenceAttributeViaUsing))]
public partial class ReferenceAttributeViaUsingTests
{
    [Test]
    public void HasDependency()
    {
        ReferenceAttributeViaUsing model = Create();

        Assert.AreEqual(_userReader.Object, model.Dependency);
    }
}
