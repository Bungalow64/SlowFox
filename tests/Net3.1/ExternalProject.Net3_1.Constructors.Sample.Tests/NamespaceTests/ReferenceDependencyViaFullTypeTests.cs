using ExternalProject.Net3_1.Constructors.Sample.InjectableDependencies;
using ExternalProject.Net3_1.Constructors.Sample.NamespaceTests;
using Moq;
using Xunit;

namespace ExternalProject.Net3_1.Constructors.Sample.Tests.NamespaceTests
{
    public class ReferenceDependencyViaFullTypeTests
    {
        [Fact]
        public void HasConstructor()
        {
            var exception = Record.Exception(() => new ReferenceDependencyViaFullType(new Mock<IUserReader>().Object));
            Assert.Null(exception);
        }
    }
}
