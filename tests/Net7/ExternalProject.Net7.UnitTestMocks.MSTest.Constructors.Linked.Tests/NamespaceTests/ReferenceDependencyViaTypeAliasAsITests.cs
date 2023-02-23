using ExternalProject.Net7.Constructors.Sample.NamespaceTests;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ExternalProject.Net7.UnitTestMocks.MSTest.Constructors.Linked.Tests.NamespaceTests;

[TestClass]
[SlowFox.InjectMocks(typeof(ReferenceDependencyViaTypeAliasAsI))]
public partial class ReferenceDependencyViaTypeAliasAsITests
{
    [TestMethod]
    public void HasDependency()
    {
        ReferenceDependencyViaTypeAliasAsI model = Create();

        Assert.AreEqual(_i.Object, model.Dependency);
    }
}
