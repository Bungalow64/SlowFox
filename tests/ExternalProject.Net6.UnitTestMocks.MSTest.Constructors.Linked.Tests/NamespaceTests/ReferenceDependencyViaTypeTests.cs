using ExternalProject.Net6.SampleProject1.NamespaceTests;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ExternalProject.Net6.UnitTestMocks.MSTest.Constructors.Linked.Tests.NamespaceTests;

[TestClass]
[SlowFox.InjectMocks(typeof(ReferenceDependencyViaType))]
public partial class ReferenceDependencyViaTypeTests
{
    [TestMethod]
    public void HasDependency()
    {
        ReferenceDependencyViaType model = Create();

        Assert.AreEqual(_userReader.Object, model.Dependency);
    }
}
