using ExternalProject.Net3_1.SampleProject1.StructureTests;
using Moq;
using Xunit;

namespace ExternalProject.Net3_1.SampleProject1.Tests.StructureTests
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
