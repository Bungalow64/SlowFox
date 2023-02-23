using ExternalProject.Net7.Constructors.Sample;

namespace ExternalProject.Net7.UnitTestMocks.MSTest.Constructors.Sample.Tests.LocalClasses;

[SlowFox.InjectDependencies(typeof(IDataReader))]
public partial class LocalFileScopedNamespace
{
    public IDataReader DataReader => _dataReader;
}
