using ExternalProject.Net7.UnitTestMocks.MSTest.Constructors.Sample.Tests.LocalClasses;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ExternalProject.Net7.UnitTestMocks.MSTest.Constructors.Sample.Tests.StructureTests;

[TestClass]
[SlowFox.InjectMocks(typeof(LocalFileScopedNamespace))]
public partial class LocalFileScopedNamespaceTests
{
    [TestMethod]
    public void HasDependency()
    {
        LocalFileScopedNamespace model = Create();

        Assert.AreEqual(_dataReader.Object, model.DataReader);
    }
}
