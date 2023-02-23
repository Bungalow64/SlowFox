using ExternalProject.Net6.Constructors.Sample.StructureTests;

namespace ExternalProject.Net6.UnitTestMocks.NUnit.Constructors.Linked.Tests.NamespaceTests;

[TestFixture]
[SlowFox.InjectMocks(typeof(NestedPublicClass.InnerClass))]
public partial class NestedPublicClassTests
{
    [Test]
    public void HasDependency()
    {
        NestedPublicClass.InnerClass model = Create();

        Assert.That(model.DataReader, Is.EqualTo(_dataReader.Object));
    }
}
