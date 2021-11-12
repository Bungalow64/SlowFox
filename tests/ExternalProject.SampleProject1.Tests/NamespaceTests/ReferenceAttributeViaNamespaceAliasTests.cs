using ExternalProject.SampleProject1.InjectableDependencies;
using ExternalProject.SampleProject1.NamespaceTests;
using Moq;
using Xunit;

namespace ExternalProject.SampleProject1.Tests.NamespaceTests
{
    public class ReferenceAttributeViaNamespaceAliasTests
    {
        [Fact]
        public void HasConstructor()
        {
            var exception = Record.Exception(() => new ReferenceAttributeViaNamespaceAlias(new Mock<IUserReader>().Object));
            Assert.Null(exception);
        }
    }
}
