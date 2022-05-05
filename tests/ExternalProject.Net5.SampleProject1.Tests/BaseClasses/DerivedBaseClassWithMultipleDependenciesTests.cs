using ExternalProject.Net5.SampleProject1.BaseClasses;
using ExternalProject.Net5.SampleProject1.InjectableDependencies;
using Moq;
using Xunit;

namespace ExternalProject.Net5.SampleProject1.Tests.BaseClasses
{
    public class DerivedBaseClassWithMultipleDependenciesTests
    {
        [Fact]
        public void HasConstructor()
        {
            var exception = Record.Exception(() => new DerivedBaseClassWithMultipleDependencies(new Mock<IUserReader>().Object, new Mock<IUserWriter>().Object, new Mock<IDataReader>().Object));
            Assert.Null(exception);
        }

        [Fact]
        public void HasDependency()
        {
            var userReader = new Mock<IUserReader>();
            var userWriter = new Mock<IUserWriter>();
            var dataReader = new Mock<IDataReader>();
            var model = new DerivedBaseClassWithMultipleDependencies(userReader.Object, userWriter.Object, dataReader.Object);

            Assert.Equal(userReader.Object, model.UserReader);
            Assert.Equal(userWriter.Object, model.UserWriter);
            Assert.Equal(dataReader.Object, model.DataReader);
        }
    }
}
