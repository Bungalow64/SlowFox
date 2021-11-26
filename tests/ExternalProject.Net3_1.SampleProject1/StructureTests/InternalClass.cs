namespace ExternalProject.Net3_1.SampleProject1.StructureTests
{
    [SlowFox.InjectDependencies(typeof(IDataReader))]
    internal partial class InternalClass
    {
        public IDataReader DataReader => _dataReader;
    }
}
