using ExternalProject.Net6.Constructors.Sample.BaseClasses;

namespace ExternalProject.Net6.UnitTestMocks.NUnit.Constructors.Linked.Tests.BaseClasses
{
    [TestFixture]
    [SlowFox.InjectMocks(typeof(DerivedBaseClassWithDeepMatchingDependency))]
    public partial class DerivedBaseClassWithDeepMatchingDependencyTests
    {
        [Test]
        public void HasDependency()
        {
            DerivedBaseClassWithDeepMatchingDependency model = Create();

            Assert.That(model.DataReader, Is.EqualTo(_dataReader.Object));
            Assert.That(model.UserWriter, Is.EqualTo(_userWriter.Object));
            Assert.That(model.UserReader, Is.EqualTo(_userReader.Object));
        }
    }
}
