using ExternalProject.Net6.UnitTestMocks.NUnit.Constructors.Sample.Tests.LocalClasses;

namespace ExternalProject.Net6.UnitTestMocks.NUnit.Constructors.Sample.Tests.StructureTests;

[TestFixture]
[SlowFox.InjectMocks(typeof(LocalFileScopedNamespace))]
public partial class LocalFileScopedNamespaceTests
{
    [Test]
    public void HasDependency()
    {
        LocalFileScopedNamespace model = Create();

        Assert.AreEqual(_dataReader.Object, model.DataReader);
    }
}
