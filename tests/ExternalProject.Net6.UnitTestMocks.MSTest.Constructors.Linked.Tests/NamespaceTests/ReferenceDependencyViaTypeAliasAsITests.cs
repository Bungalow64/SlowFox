using ExternalProject.Net6.Constructors.Sample.NamespaceTests;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ExternalProject.Net6.UnitTestMocks.MSTest.Constructors.Linked.Tests.NamespaceTests;

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
