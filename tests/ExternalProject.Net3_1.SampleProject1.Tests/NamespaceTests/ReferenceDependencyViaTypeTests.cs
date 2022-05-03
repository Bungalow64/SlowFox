using ExternalProject.Net3_1.SampleProject1.InjectableDependencies;
using ExternalProject.Net3_1.SampleProject1.NamespaceTests;
using Moq;
using Xunit;

namespace ExternalProject.Net3_1.SampleProject1.Tests.NamespaceTests
{
    public class ReferenceDependencyViaTypeTests
    {
        [Fact]
        public void HasConstructor()
        {
            var exception = Record.Exception(() => new ReferenceDependencyViaType(new Mock<IUserReader>().Object));
            Assert.Null(exception);
        }
    }
}
