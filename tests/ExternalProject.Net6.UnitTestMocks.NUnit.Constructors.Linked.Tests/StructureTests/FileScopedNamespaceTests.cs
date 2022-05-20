using ExternalProject.Net6.Constructors.Sample.StructureTests;

namespace ExternalProject.Net6.UnitTestMocks.NUnit.Constructors.Linked.Tests.NamespaceTests;

[TestFixture]
[SlowFox.InjectMocks(typeof(FileScopedNamespace))]
public partial class FileScopedNamespaceTests
{
    [Test]
    public void HasDependency()
    {
        FileScopedNamespace model = Create();

        Assert.That(model.DataReader, Is.EqualTo(_dataReader.Object));
    }
}
