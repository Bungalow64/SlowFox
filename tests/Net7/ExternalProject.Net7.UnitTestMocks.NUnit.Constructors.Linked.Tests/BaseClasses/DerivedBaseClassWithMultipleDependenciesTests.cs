using ExternalProject.Net7.Constructors.Sample.BaseClasses;

namespace ExternalProject.Net7.UnitTestMocks.NUnit.Constructors.Linked.Tests.BaseClasses
{
    [TestFixture]
    [SlowFox.InjectMocks(typeof(DerivedBaseClassWithMultipleDependencies))]
    public partial class DerivedBaseClassWithMultipleDependenciesTestse
    {
        [Test]
        public void HasDependency()
        {
            DerivedBaseClassWithMultipleDependencies model = Create();

            Assert.That(model.UserWriter, Is.EqualTo(_userWriter.Object));
            Assert.That(model.UserReader, Is.EqualTo(_userReader.Object));
            Assert.That(model.DataReader, Is.EqualTo(_dataReader.Object));
        }
    }
}
