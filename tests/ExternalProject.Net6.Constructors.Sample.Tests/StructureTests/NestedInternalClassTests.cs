using ExternalProject.Net6.Constructors.Sample.StructureTests;
using Moq;
using Xunit;

namespace ExternalProject.Net6.Constructors.Sample.Tests.StructureTests
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
