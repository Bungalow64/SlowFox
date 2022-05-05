using ExternalProject.Net6.SampleProject1.BaseClasses;
using ExternalProject.Net6.SampleProject1.InjectableDependencies;
using Moq;
using Xunit;

namespace ExternalProject.Net6.SampleProject1.Tests.BaseClasses
{
    public class DerivedBaseClassWithNestedDependencyTests
    {
        [Fact]
        public void HasConstructor()
        {
            var exception = Record.Exception(() => new DerivedBaseClassWithNestedDependency(new Mock<IUserWriter>().Object, new Mock<IUserReader>().Object, new Mock<IDataReader>().Object));
            Assert.Null(exception);
        }

        [Fact]
        public void HasDependency()
        {
            var userWriter = new Mock<IUserWriter>();
            var userReader = new Mock<IUserReader>();
            var dataReader = new Mock<IDataReader>();
            var model = new DerivedBaseClassWithNestedDependency(userWriter.Object, userReader.Object, dataReader.Object);

            Assert.Equal(userReader.Object, model.UserReader);
            Assert.Equal(userWriter.Object, model.UserWriter);
            Assert.Equal(dataReader.Object, model.DataReader);
        }
    }
}
