using ExternalProject.Net5.UnitTestMocks.xUnit.Constructors.Sample.Tests.LocalClasses;
using Xunit;

namespace ExternalProject.Net5.UnitTestMocks.xUnit.Constructors.Sample.Tests.StructureTests
{
    [SlowFox.InjectMocks(typeof(LocalFileScopedNamespace))]
    public partial class LocalFileScopedNamespaceTests
    {
        [Fact]
        public void HasDependency()
        {
            LocalFileScopedNamespace model = Create();

            Assert.Equal(_dataReader.Object, model.DataReader);
        }
    }
}