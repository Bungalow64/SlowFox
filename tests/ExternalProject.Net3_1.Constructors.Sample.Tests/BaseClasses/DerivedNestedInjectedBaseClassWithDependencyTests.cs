using ExternalProject.Net3_1.Constructors.Sample.BaseClasses;
using ExternalProject.Net3_1.Constructors.Sample.InjectableDependencies;
using Moq;
using Xunit;

namespace ExternalProject.Net3_1.Constructors.Sample.Tests.BaseClasses
{
    public class DerivedNestedInjectedBaseClassWithDependencyTests
    {
        [Fact]
        public void HasConstructor()
        {
            var exception = Record.Exception(() => new DerivedNestedInjectedBaseClassWithDependency(new Mock<IUserReader>().Object, new Mock<IUserWriter>().Object, new Mock<IDataReader2>().Object));
            Assert.Null(exception);
        }

        [Fact]
        public void HasDependency()
        {
            var userReader = new Mock<IUserReader>();
            var userWriter = new Mock<IUserWriter>();
            var dataReader2 = new Mock<IDataReader2>();
            var model = new DerivedNestedInjectedBaseClassWithDependency(userReader.Object, userWriter.Object, dataReader2.Object);

            Assert.Equal(userReader.Object, model.UserReader);
            Assert.Equal(userWriter.Object, model.UserWriter);
            Assert.Equal(dataReader2.Object, model.DataReader2);
        }
    }
}
