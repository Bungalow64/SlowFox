using ExternalProject.Net5.Constructors.Sample.StructureTests;
using Moq;
using Xunit;

namespace ExternalProject.Net5.Constructors.Sample.Tests.StructureTests
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
