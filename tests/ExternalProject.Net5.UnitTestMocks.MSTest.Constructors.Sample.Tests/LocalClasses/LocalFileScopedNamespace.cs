using ExternalProject.Net5.SampleProject1;

namespace ExternalProject.Net5.UnitTestMocks.MSTest.Constructors.Sample.Tests.LocalClasses
{

    [SlowFox.InjectDependencies(typeof(IDataReader))]
    public partial class LocalFileScopedNamespace
    {
        public IDataReader DataReader => _dataReader;
    }
}