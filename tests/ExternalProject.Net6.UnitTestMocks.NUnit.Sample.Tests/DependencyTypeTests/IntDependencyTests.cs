using ExternalProject.Net6.UnitTestMocks.Sample.DependencyTypeTests;

namespace ExternalProject.Net6.UnitTestMocks.NUnit.Sample.Tests.DependencyTypeTests
{
    [TestFixture]
    [SlowFox.InjectMocks(typeof(IntDependency))]
    public partial class IntDependencyTests
    {
        [Test]
        public void Create_ObjectsExist()
        {
            const int index = 1001;
            IntDependency model = Create(index);

            Assert.IsNotNull(model);
            Assert.IsNotNull(_userReader);
        }

        [Test]
        public void Mock_CanMock()
        {
            _userReader.Setup(p => p.GetName()).Returns("Jamie");

            const int index = 1001;
            var model = Create(index);

            Assert.AreEqual("Jamie", model.GetName());
            _userReader.Verify(p => p.GetName(), Moq.Times.Once);

            Assert.AreEqual(index, model.GetIndex());
        }
    }
}
