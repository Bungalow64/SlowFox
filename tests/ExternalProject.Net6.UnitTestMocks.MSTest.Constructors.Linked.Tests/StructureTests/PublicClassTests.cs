using ExternalProject.Net6.SampleProject1.StructureTests;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ExternalProject.Net6.UnitTestMocks.MSTest.Constructors.Linked.Tests.NamespaceTests;

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
