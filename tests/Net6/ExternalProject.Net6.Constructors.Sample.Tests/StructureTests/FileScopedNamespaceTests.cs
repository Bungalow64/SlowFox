using ExternalProject.Net6.Constructors.Sample.StructureTests;
using Moq;
using Xunit;

namespace ExternalProject.Net6.Constructors.Sample.Tests.StructureTests
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
