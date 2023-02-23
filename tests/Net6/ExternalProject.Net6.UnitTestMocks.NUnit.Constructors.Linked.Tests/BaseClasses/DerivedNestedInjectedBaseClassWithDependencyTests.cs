using ExternalProject.Net6.Constructors.Sample.BaseClasses;

namespace ExternalProject.Net6.UnitTestMocks.NUnit.Constructors.Linked.Tests.BaseClasses
{
    [TestFixture]
    [SlowFox.InjectMocks(typeof(DerivedNestedInjectedBaseClassWithDependency))]
    public partial class DerivedNestedInjectedBaseClassWithDependencyTests
    {
        [Test]
        public void HasDependency()
        {
            DerivedNestedInjectedBaseClassWithDependency model = Create();

            Assert.That(model.UserWriter, Is.EqualTo(_userWriter.Object));
            Assert.That(model.UserReader, Is.EqualTo(_userReader.Object));
            Assert.That(model.DataReader2, Is.EqualTo(_dataReader2.Object));
        }
    }
}
