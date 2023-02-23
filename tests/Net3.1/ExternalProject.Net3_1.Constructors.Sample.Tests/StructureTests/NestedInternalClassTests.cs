using ExternalProject.Net3_1.Constructors.Sample.StructureTests;
using Moq;
using Xunit;

namespace ExternalProject.Net3_1.Constructors.Sample.Tests.StructureTests
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
