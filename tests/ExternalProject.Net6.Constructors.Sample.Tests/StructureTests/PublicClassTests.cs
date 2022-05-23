using ExternalProject.Net6.Constructors.Sample.StructureTests;
using Moq;
using Xunit;

namespace ExternalProject.Net6.Constructors.Sample.Tests.StructureTests
{
    public class PublicClassTests
    {
        [Fact]
        public void NestedClassHasConstructor()
        {
            var exception = Record.Exception(() => new PublicClass(new Mock<IDataReader>().Object));
            Assert.Null(exception);
        }
    }
}
