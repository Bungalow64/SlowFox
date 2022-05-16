using ExternalProject.Net6.Constructors.Sample.BaseClasses;
using ExternalProject.Net6.Constructors.Sample.InjectableDependencies;
using Moq;
using Xunit;

namespace ExternalProject.Net6.Constructors.Sample.Tests.BaseClasses
{
    public class DerivedBaseClassUsingNoneInjectedTests
    {
        [Fact]
        public void HasConstructor()
        {
            var exception = Record.Exception(() => new DerivedBaseClassUsingNoneInjected(new Mock<IUserReader>().Object, new Mock<IDataReader>().Object, new Mock<IDataReader2>().Object));
            Assert.Null(exception);
        }

        [Fact]
        public void HasDependency()
        {
            var userReader = new Mock<IUserReader>();
            var dataReader = new Mock<IDataReader>();
            var dataReader2 = new Mock<IDataReader2>();
            var model = new DerivedBaseClassUsingNoneInjected(userReader.Object, dataReader.Object, dataReader2.Object);

            Assert.Equal(userReader.Object, model.UserReader);
            Assert.Equal(dataReader.Object, model.DataReader);
            Assert.Equal(dataReader2.Object, model.DataReader2);
        }
    }
}
