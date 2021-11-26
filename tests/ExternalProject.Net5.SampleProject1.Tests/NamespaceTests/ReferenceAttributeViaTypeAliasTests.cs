using ExternalProject.Net5.SampleProject1.InjectableDependencies;
using ExternalProject.Net5.SampleProject1.NamespaceTests;
using Moq;
using Xunit;

namespace ExternalProject.Net5.SampleProject1.Tests.NamespaceTests
{
    public class ReferenceAttributeViaTypeAliasTests
    {
        [Fact]
        public void HasConstructor()
        {
            var exception = Record.Exception(() => new ReferenceAttributeViaTypeAlias(new Mock<IUserReader>().Object));
            Assert.Null(exception);
        }
    }
}
