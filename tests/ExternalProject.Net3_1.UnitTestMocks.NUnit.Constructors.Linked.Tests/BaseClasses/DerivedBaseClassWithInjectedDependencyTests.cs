using ExternalProject.Net3_1.Constructors.Sample.BaseClasses;
using NUnit.Framework;

namespace ExternalProject.Net3_1.UnitTestMocks.NUnit.Constructors.Linked.Tests.BaseClasses
{
    [TestFixture]
    [SlowFox.InjectMocks(typeof(DerivedBaseClassWithInjectedDependency))]
    public partial class DerivedBaseClassWithInjectedDependencyTests
    {
        [Test]
        public void HasDependency()
        {
            DerivedBaseClassWithInjectedDependency model = Create();

            Assert.That(model.UserWriter, Is.EqualTo(_userWriter.Object));
            Assert.That(model.UserReader, Is.EqualTo(_userReader.Object));
        }
    }
}
