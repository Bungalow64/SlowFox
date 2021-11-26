namespace ExternalProject.Net6.SampleProject1.StructureTests;

[SlowFox.InjectDependencies(typeof(IDataReader))]
public partial class FileScopedNamespace
{
    public IDataReader DataReader => _dataReader;
}
