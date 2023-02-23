using ExternalProject.Net6.Constructors.Sample;

namespace ExternalProject.Net6.UnitTestMocks.NUnit.Constructors.Sample.Tests.LocalClasses;

[SlowFox.InjectDependencies(typeof(IDataReader))]
public partial class LocalFileScopedNamespace
{
    public IDataReader DataReader => _dataReader;
}
