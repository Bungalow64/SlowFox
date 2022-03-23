using ExternalProject.Net3_1.UnitTestMocks.MSTest.Sample.NamespaceTests;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace ExternalProject.Net3_1.UnitTestMocks.MSTest.Sample.MultipleDependenciesTests.Tests
{
    [SlowFox.InjectMocks(typeof(TwoDependencies))]
    [TestClass]
    public partial class TwoDependenciesTests
    {
        [TestMethod]
        public void Create_ObjectsExist()
        {
            TwoDependencies model = Create();

            Assert.IsNotNull(model);
            Assert.IsNotNull(_userReader);
            Assert.IsNotNull(_userWriter);
        }

        [TestMethod]
        public void Mock_CanMock()
        {
            string? requestedName = null;

            _userReader.Setup(p => p.GetName()).Returns("Jamie");

            _userWriter
                .Setup(p => p.UpdateName(It.IsAny<string>()))
                .Callback<string>(p => requestedName = p);

            var model = Create();
            model.UpdateName("Jamie2");

            Assert.AreEqual("Jamie", model.GetName());
            Assert.AreEqual("Jamie2", requestedName);

            _userReader.Verify(p => p.GetName(), Times.Once);
            _userWriter.Verify(p => p.UpdateName(It.IsAny<string>()), Times.Once);
        }
    }
}