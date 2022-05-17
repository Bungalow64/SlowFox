using NUnit.Framework;
using ExternalProject.Net5.UnitTestMocks.Sample.MultipleDependenciesTests;

namespace ExternalProject.Net5.UnitTestMocks.NUnit.Sample.MultipleDependenciesTests
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

            Assert.AreEqual("Jamie1", model.GetName1());
            Assert.AreEqual("Jamie2", model.GetName2());

            _userReader1.Verify(p => p.GetName(), Moq.Times.Once);
            _userReader2.Verify(p => p.GetName(), Moq.Times.Once);
        }
    }
}