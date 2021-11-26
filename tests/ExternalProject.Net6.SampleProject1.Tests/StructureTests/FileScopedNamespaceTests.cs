using ExternalProject.Net6.SampleProject1.StructureTests;
using Moq;
using Xunit;

namespace ExternalProject.Net6.SampleProject1.Tests.StructureTests
{
    public class FileScopedNamespaceTests
    {
        [Fact]
        public void ClassHasConstructor()
        {
            var exception = Record.Exception(() => new FileScopedNamespace(new Mock<IDataReader>().Object));
            Assert.Null(exception);
        }
    }
}
