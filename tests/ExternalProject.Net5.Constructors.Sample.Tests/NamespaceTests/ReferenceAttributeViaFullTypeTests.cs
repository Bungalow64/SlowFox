using ExternalProject.Net5.Constructors.Sample.InjectableDependencies;
using ExternalProject.Net5.Constructors.Sample.NamespaceTests;
using Moq;
using Xunit;

namespace ExternalProject.Net5.Constructors.Sample.Tests.NamespaceTests
{
    public class ReferenceAttributeViaFullTypeTests
    {
        [Fact]
        public void HasConstructor()
        {
            var exception = Record.Exception(() => new ReferenceAttributeViaFullType(new Mock<IUserReader>().Object));
            Assert.Null(exception);
        }
    }
}
