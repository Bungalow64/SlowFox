using ExternalProject.Net6.Constructors.Sample.StructureTests;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ExternalProject.Net6.UnitTestMocks.MSTest.Constructors.Linked.Tests.NamespaceTests;

[TestClass]
[SlowFox.InjectMocks(typeof(FileScopedNamespace))]
public partial class FileScopedNamespaceTests
{
    [TestMethod]
    public void HasDependency()
    {
        FileScopedNamespace model = Create();

        Assert.AreEqual(_dataReader.Object, model.DataReader);
    }
}
