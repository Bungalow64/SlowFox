using ExternalProject.Net6.SampleProject1.NamespaceTests;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ExternalProject.Net6.UnitTestMocks.MSTest.Constructors.Linked.Tests.NamespaceTests;

[TestClass]
[SlowFox.InjectMocks(typeof(ReferenceDependencyViaFullType))]
public partial class ReferenceDependencyViaFullTypeTests
{
    [TestMethod]
    public void HasDependency()
    {
        ReferenceDependencyViaFullType model = Create();

        Assert.AreEqual(_userReader.Object, model.Dependency);
    }
}
