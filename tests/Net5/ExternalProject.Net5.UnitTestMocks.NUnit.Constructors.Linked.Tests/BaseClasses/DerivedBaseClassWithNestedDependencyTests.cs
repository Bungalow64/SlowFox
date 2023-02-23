using ExternalProject.Net5.Constructors.Sample.BaseClasses;
using NUnit.Framework;

namespace ExternalProject.Net5.UnitTestMocks.NUnit.Constructors.Linked.Tests.BaseClasses
{
    [TestFixture]
    [SlowFox.InjectMocks(typeof(DerivedBaseClassWithNestedDependency))]
    public partial class DerivedBaseClassWithNestedDependencyTests
    {
        [Test]
        public void HasDependency()
        {
            DerivedBaseClassWithNestedDependency model = Create();

            Assert.That(model.UserWriter, Is.EqualTo(_userWriter.Object));
            Assert.That(model.UserReader, Is.EqualTo(_userReader.Object));
            Assert.That(model.DataReader, Is.EqualTo(_dataReader.Object));
        }
    }
}
