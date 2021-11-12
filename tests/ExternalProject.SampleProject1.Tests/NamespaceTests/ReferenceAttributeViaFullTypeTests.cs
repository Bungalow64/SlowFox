using ExternalProject.SampleProject1.InjectableDependencies;
using ExternalProject.SampleProject1.NamespaceTests;
using Moq;
using Xunit;

namespace ExternalProject.SampleProject1.Tests.NamespaceTests
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
