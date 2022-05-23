using ExternalProject.Net5.Constructors.Sample.BaseClasses;
using Xunit;

namespace ExternalProject.Net5.UnitTestMocks.xUnit.Constructors.Linked.Tests.BaseClasses
{
    [SlowFox.InjectMocks(typeof(DerivedBaseClassWithDependencyAlias))]
    public partial class DerivedBaseClassWithDependencyAliasTests
    {
        [Fact]
        public void HasDependency()
        {
            DerivedBaseClassWithDependencyAlias model = Create();

            Assert.Equal(_userWriter.Object, model.UserWriter);
            Assert.Equal(_userReader.Object, model.UserReader);
        }
    }
}
