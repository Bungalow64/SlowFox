namespace ExternalProject.SampleProject1.StructureTests
{
    [SlowFox.InjectDependencies(typeof(IDataReader))]
    public partial class PublicClass
    {
        public IDataReader DataReader => _dataReader;
    }
}
