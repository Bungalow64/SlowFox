using ExternalProject.Net3_1.SampleProject1.BaseClasses;
using ExternalProject.Net3_1.SampleProject1.InjectableDependencies;
using Moq;
using Xunit;

namespace ExternalProject.Net3_1.SampleProject1.Tests.BaseClasses
{
    public class DerivedBaseClassWithDeepMatchingDependencyTests
    {
        [Fact]
        public void HasConstructor()
        {
            var exception = Record.Exception(() => new DerivedBaseClassWithDeepMatchingDependency(new Mock<IUserReader>().Object, new Mock<IDataReader>().Object, new Mock<IUserWriter>().Object));
            Assert.Null(exception);
        }

        [Fact]
        public void HasDependency()
        {
            var userReader = new Mock<IUserReader>();
            var userWriter = new Mock<IUserWriter>();
            var dataReader = new Mock<IDataReader>();
            var model = new DerivedBaseClassWithDeepMatchingDependency(userReader.Object, dataReader.Object, userWriter.Object);

            Assert.Equal(userReader.Object, model.UserReader);
            Assert.Equal(userWriter.Object, model.UserWriter);
            Assert.Equal(dataReader.Object, model.DataReader);
        }
    }
}
