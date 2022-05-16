using ExternalProject.Net3_1.Constructors.Sample.StructureTests;
using Moq;
using Xunit;

namespace ExternalProject.Net3_1.Constructors.Sample.Tests.StructureTests
{
    public class NestedProtectedClassTests
    {
        [Fact]
        public void NestedClassHasConstructor()
        {
            Assert.True(NestedProtectedClass.CheckInnerClass(new Mock<IDataReader>().Object));
        }
    }
}
