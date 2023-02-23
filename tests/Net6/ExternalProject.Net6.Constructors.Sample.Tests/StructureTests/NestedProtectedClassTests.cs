using ExternalProject.Net6.Constructors.Sample.StructureTests;
using Moq;
using Xunit;

namespace ExternalProject.Net6.Constructors.Sample.Tests.StructureTests
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
