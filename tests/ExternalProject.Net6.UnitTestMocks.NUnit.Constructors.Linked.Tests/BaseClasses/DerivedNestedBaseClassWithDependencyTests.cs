using ExternalProject.Net6.Constructors.Sample.BaseClasses;

namespace ExternalProject.Net6.UnitTestMocks.NUnit.Constructors.Linked.Tests.BaseClasses
{
    [TestFixture]
    [SlowFox.InjectMocks(typeof(DerivedNestedBaseClassWithDependency))]
    public partial class DerivedNestedBaseClassWithDependencyTests
    {
        [Test]
        public void HasDependency()
        {
            DerivedNestedBaseClassWithDependency model = Create();

            Assert.AreEqual(_userWriter.Object, model.UserWriter);
            Assert.AreEqual(_userReader.Object, model.UserReader);
            Assert.AreEqual(_dataReader.Object, model.DataReader);
            Assert.AreEqual(_dataReader2.Object, model.DataReader2);
        }
    }
}
