using NUnit.Framework;
using ExternalProject.Net3_1.UnitTestMocks.Sample.MultipleDependenciesTests;

namespace ExternalProject.Net3_1.UnitTestMocks.NUnit.Sample.MultipleDependenciesTests
{
    [SlowFox.InjectMocks(typeof(DuplicateDependency))]
    [TestFixture]
    public partial class DuplicateDependencyTests
    {
        [Test]
        public void Create_ObjectsExist()
        {
            DuplicateDependency model = Create();

            Assert.IsNotNull(model);
            Assert.IsNotNull(_userReader1);
            Assert.IsNotNull(_userReader2);
            Assert.IsNotNull(_userWriter);
        }

        [Test]
        public void Mock_CanMock()
        {
            _userReader1.Setup(p => p.GetName()).Returns("Jamie1");
            _userReader2.Setup(p => p.GetName()).Returns("Jamie2");

            var model = Create();

            Assert.That(model.GetName1(), Is.EqualTo("Jamie1"));
            Assert.That(model.GetName2(), Is.EqualTo("Jamie2"));

            _userReader1.Verify(p => p.GetName(), Moq.Times.Once);
            _userReader2.Verify(p => p.GetName(), Moq.Times.Once);
        }
    }
}