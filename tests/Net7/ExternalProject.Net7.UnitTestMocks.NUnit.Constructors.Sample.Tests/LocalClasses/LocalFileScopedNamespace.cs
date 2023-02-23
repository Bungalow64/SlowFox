using ExternalProject.Net7.Constructors.Sample;

namespace ExternalProject.Net7.UnitTestMocks.NUnit.Constructors.Sample.Tests.LocalClasses;

[SlowFox.InjectDependencies(typeof(IDataReader))]
public partial class LocalFileScopedNamespace
{
    public IDataReader DataReader => _dataReader;
}
