using ExternalProject.Net5.Constructors.Sample.BaseClasses;
using ExternalProject.Net5.Constructors.Sample.InjectableDependencies;
using Moq;
using Xunit;

namespace ExternalProject.Net5.Constructors.Sample.Tests.BaseClasses
{
    public class DerivedBaseClassWithDependencyAliasPartialNamespaceTests
    {
        [Fact]
        public void HasConstructor()
        {
            var exception = Record.Exception(() => new DerivedBaseClassWithDependencyAliasPartialNamespace(new Mock<IUserReader>().Object, new Mock<IUserWriter>().Object));
            Assert.Null(exception);
        }

        [Fact]
        public void HasDependency()
        {
            var userReader = new Mock<IUserReader>();
            var userWriter = new Mock<IUserWriter>();
            var model = new DerivedBaseClassWithDependencyAliasPartialNamespace(userReader.Object, userWriter.Object);

            Assert.Equal(userReader.Object, model.UserReader);
            Assert.Equal(userWriter.Object, model.UserWriter);
        }
    }
}
