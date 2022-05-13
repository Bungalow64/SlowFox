using ExternalProject.Net6.SampleProject1;

namespace ExternalProject.Net6.UnitTestMocks.MSTest.Constructors.Sample.Tests.LocalClasses;

[SlowFox.InjectDependencies(typeof(IDataReader))]
public partial class LocalFileScopedNamespace
{
    public IDataReader DataReader => _dataReader;
}
