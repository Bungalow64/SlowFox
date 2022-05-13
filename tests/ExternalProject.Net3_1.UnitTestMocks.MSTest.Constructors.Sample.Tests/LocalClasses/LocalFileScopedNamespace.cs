using ExternalProject.Net3_1.SampleProject1;

namespace ExternalProject.Net3_1.UnitTestMocks.MSTest.Constructors.Sample.Tests.LocalClasses
{
    [SlowFox.InjectDependencies(typeof(IDataReader))]
    public partial class LocalFileScopedNamespace
    {
        public IDataReader DataReader => _dataReader;
    }
}