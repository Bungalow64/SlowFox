using ExternalProject.Net7.Constructors.Sample.StructureTests;
using Moq;
using Xunit;

namespace ExternalProject.Net7.Constructors.Sample.Tests.StructureTests
{
    public class NestedPublicClassTests
    {
        [Fact]
        public void NestedClassHasConstructor()
        {
            var exception = Record.Exception(() => new NestedPublicClass.InnerClass(new Mock<IDataReader>().Object));
            Assert.Null(exception);
        }
        [Fact]
        public void NestedClassHasProperty()
        {
            var model = new NestedPublicClass.InnerClass(new Mock<IDataReader>().Object);
            Assert.NotNull(model.DataReader);
        }
    }
}
