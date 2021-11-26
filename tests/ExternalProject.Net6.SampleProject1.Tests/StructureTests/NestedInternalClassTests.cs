using ExternalProject.Net6.SampleProject1.StructureTests;
using Moq;
using Xunit;

namespace ExternalProject.Net6.SampleProject1.Tests.StructureTests
{
    public class NestedInternalClassTests
    {
        [Fact]
        public void NestedClassHasConstructor()
        {
            var model = new NestedInternalClass();
            Assert.True(NestedInternalClass.CheckInnerClass(new Mock<IDataReader>().Object));
        }
    }
}
