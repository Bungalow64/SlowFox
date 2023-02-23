using ExternalProject.Net7.Constructors.Sample.StructureTests;

namespace ExternalProject.Net7.UnitTestMocks.NUnit.Constructors.Linked.Tests.NamespaceTests;

[TestFixture]
[SlowFox.InjectMocks(typeof(PublicClass))]
public partial class PublicClassTests
{
    [Test]
    public void HasDependency()
    {
        PublicClass model = Create();

        Assert.That(model.DataReader, Is.EqualTo(_dataReader.Object));
    }
}
