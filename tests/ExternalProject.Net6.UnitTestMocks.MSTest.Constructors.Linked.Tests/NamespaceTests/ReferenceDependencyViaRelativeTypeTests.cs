using ExternalProject.Net6.SampleProject1.NamespaceTests;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ExternalProject.Net6.UnitTestMocks.MSTest.Constructors.Linked.Tests.NamespaceTests;

[TestClass]
[SlowFox.InjectMocks(typeof(ReferenceDependencyViaRelativeType))]
public partial class ReferenceDependencyViaRelativeTypeTests
{
    [TestMethod]
    public void HasDependency()
    {
        ReferenceDependencyViaRelativeType model = Create();

        Assert.AreEqual(_userReader.Object, model.Dependency);
    }
}
