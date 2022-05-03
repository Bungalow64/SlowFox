using ExternalProject.Net5.SampleProject1.InjectableDependencies;
using ExternalProject.Net5.SampleProject1.NamespaceTests;
using Moq;
using Xunit;

namespace ExternalProject.Net5.SampleProject1.Tests.NamespaceTests
{
    public class ReferenceDependencyViaTypeAliasTests
    {
        [Fact]
        public void HasConstructor()
        {
            var exception = Record.Exception(() => new ReferenceDependencyViaTypeAlias(new Mock<IUserReader>().Object));
            Assert.Null(exception);
        }
    }
}
