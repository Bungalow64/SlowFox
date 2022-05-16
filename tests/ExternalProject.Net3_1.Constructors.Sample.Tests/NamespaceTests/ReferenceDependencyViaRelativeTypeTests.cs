using ExternalProject.Net3_1.Constructors.Sample.InjectableDependencies;
using ExternalProject.Net3_1.Constructors.Sample.NamespaceTests;
using Moq;
using Xunit;

namespace ExternalProject.Net3_1.Constructors.Sample.Tests.NamespaceTests
{
    public class ReferenceDependencyViaRelativeTypeTests
    {
        [Fact]
        public void HasConstructor()
        {
            var exception = Record.Exception(() => new ReferenceDependencyViaRelativeType(new Mock<IUserReader>().Object));
            Assert.Null(exception);
        }
    }
}
