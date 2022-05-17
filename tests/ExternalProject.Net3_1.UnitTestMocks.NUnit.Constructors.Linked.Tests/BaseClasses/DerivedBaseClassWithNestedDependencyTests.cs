using ExternalProject.Net3_1.Constructors.Sample.BaseClasses;
using NUnit.Framework;

namespace ExternalProject.Net3_1.UnitTestMocks.NUnit.Constructors.Linked.Tests.BaseClasses
{
    [TestFixture]
    [SlowFox.InjectMocks(typeof(DerivedBaseClassWithNestedDependency))]
    public partial class DerivedBaseClassWithNestedDependencyTests
    {
        [Test]
        public void HasDependency()
        {
            DerivedBaseClassWithNestedDependency model = Create();

            Assert.AreEqual(_userWriter.Object, model.UserWriter);
            Assert.AreEqual(_userReader.Object, model.UserReader);
            Assert.AreEqual(_dataReader.Object, model.DataReader);
        }
    }
}
