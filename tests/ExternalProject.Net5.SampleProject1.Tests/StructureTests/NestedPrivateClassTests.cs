using ExternalProject.Net5.SampleProject1.StructureTests;
using Moq;
using Xunit;

namespace ExternalProject.Net5.SampleProject1.Tests.StructureTests
{
    public class NestedPrivateClassTestse
    {
        [Fact]
        public void NestedClassHasConstructor()
        {
            var model = new NestedPrivateClass();
            Assert.True(NestedPrivateClass.CheckInnerClass(new Mock<IDataReader>().Object));
        }
    }
}
