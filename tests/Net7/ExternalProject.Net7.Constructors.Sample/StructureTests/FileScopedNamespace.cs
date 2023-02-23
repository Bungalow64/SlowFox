namespace ExternalProject.Net7.Constructors.Sample.StructureTests;

[SlowFox.InjectDependencies(typeof(IDataReader))]
public partial class FileScopedNamespace
{
    public IDataReader DataReader => _dataReader;
}
