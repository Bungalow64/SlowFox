using ExternalProject.Net6.Constructors.Sample.InjectableDependencies;
using ExternalProject.Net6.Constructors.Sample.NamespaceTests;
using Moq;
using Xunit;

namespace ExternalProject.Net6.Constructors.Sample.Tests.NamespaceTests
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
