using ExternalProject.Net7.Constructors.Sample.StructureTests;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ExternalProject.Net7.UnitTestMocks.MSTest.Constructors.Linked.Tests.NamespaceTests;

[TestClass]
[SlowFox.InjectMocks(typeof(PublicClass))]
public partial class PublicClassTests
{
    [TestMethod]
    public void HasDependency()
    {
        PublicClass model = Create();

        Assert.AreEqual(_dataReader.Object, model.DataReader);
    }
}
