using ExternalProject.Net7.Constructors.Sample.BaseClasses;

namespace ExternalProject.Net7.UnitTestMocks.NUnit.Constructors.Linked.Tests.BaseClasses;

[TestFixture]
[SlowFox.InjectMocks(typeof(DerivedBaseClassUsingNoneInjected))]
public partial class DerivedBaseClassUsingNoneInjectedTests
{
    [Test]
    public void HasDependency()
    {
        DerivedBaseClassUsingNoneInjected model = Create();

        Assert.That(model.DataReader, Is.EqualTo(_dataReader.Object));
        Assert.That(model.DataReader2, Is.EqualTo(_dataReader2.Object));
        Assert.That(model.UserReader, Is.EqualTo(_userReader.Object));
    }
}
