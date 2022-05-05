using ExternalProject.Net6.SampleProject1.BaseClasses;
using ExternalProject.Net6.SampleProject1.InjectableDependencies;
using Moq;
using Xunit;

namespace ExternalProject.Net6.SampleProject1.Tests.BaseClasses
{
    public class DerivedBaseClassWithInjectedDependencyTests
    {
        [Fact]
        public void HasConstructor()
        {
            var exception = Record.Exception(() => new DerivedBaseClassWithInjectedDependency(new Mock<IUserReader>().Object, new Mock<IUserWriter>().Object));
            Assert.Null(exception);
        }

        [Fact]
        public void HasDependency()
        {
            var userReader = new Mock<IUserReader>();
            var userWriter = new Mock<IUserWriter>();
            var model = new DerivedBaseClassWithInjectedDependency(userReader.Object, userWriter.Object);

            Assert.Equal(userWriter.Object, model.UserWriter);
            Assert.Equal(userReader.Object, model.UserReader);
        }
    }
}
