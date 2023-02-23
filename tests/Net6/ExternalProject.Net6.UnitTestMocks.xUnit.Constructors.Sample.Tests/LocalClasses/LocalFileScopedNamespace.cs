using ExternalProject.Net6.Constructors.Sample;

namespace ExternalProject.Net6.UnitTestMocks.xUnit.Constructors.Sample.Tests.LocalClasses;

[SlowFox.InjectDependencies(typeof(IDataReader))]
public partial class LocalFileScopedNamespace
{
    public IDataReader DataReader => _dataReader;
}
