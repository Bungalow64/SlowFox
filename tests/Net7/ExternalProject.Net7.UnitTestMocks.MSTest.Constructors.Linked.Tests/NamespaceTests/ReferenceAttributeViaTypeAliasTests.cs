using ExternalProject.Net7.Constructors.Sample.NamespaceTests;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ExternalProject.Net7.UnitTestMocks.MSTest.Constructors.Linked.Tests.NamespaceTests;

[TestClass]
[SlowFox.InjectMocks(typeof(ReferenceAttributeViaTypeAlias))]
public partial class ReferenceAttributeViaTypeAliasTests
{
    [TestMethod]
    public void HasDependency()
    {
        ReferenceAttributeViaTypeAlias model = Create();

        Assert.AreEqual(_userReader.Object, model.Dependency);
    }
}
