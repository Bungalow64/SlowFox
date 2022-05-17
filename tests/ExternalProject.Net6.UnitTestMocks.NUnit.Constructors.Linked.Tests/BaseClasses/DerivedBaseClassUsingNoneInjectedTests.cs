using ExternalProject.Net6.Constructors.Sample.BaseClasses;

namespace ExternalProject.Net6.UnitTestMocks.NUnit.Constructors.Linked.Tests.BaseClasses;

[TestFixture]
[SlowFox.InjectMocks(typeof(DerivedBaseClassUsingNoneInjected))]
public partial class DerivedBaseClassUsingNoneInjectedTests
{
    [Test]
    public void HasDependency()
    {
        DerivedBaseClassUsingNoneInjected model = Create();

        Assert.AreEqual(_dataReader.Object, model.DataReader);
        Assert.AreEqual(_dataReader2.Object, model.DataReader2);
        Assert.AreEqual(_userReader.Object, model.UserReader);
    }
}
