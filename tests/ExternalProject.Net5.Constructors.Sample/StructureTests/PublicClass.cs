namespace ExternalProject.Net5.Constructors.Sample.StructureTests
{
    [SlowFox.InjectDependencies(typeof(IDataReader))]
    public partial class PublicClass
    {
        public IDataReader DataReader => _dataReader;
    }
}
