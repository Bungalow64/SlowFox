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

            Assert.AreEqual(_dataReader.Object, model.DataReader);
            Assert.AreEqual(_userWriter.Object, model.UserWriter);
            Assert.AreEqual(_userReader.Object, model.UserReader);
        }
    }
}
