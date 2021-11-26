using ExternalProject.Net6.SampleProject1.StructureTests;
using Moq;
using Xunit;

namespace ExternalProject.Net6.SampleProject1.Tests.StructureTests
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
