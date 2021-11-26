namespace ExternalProject.Net5.SampleProject1.StructureTests
{
    [SlowFox.InjectDependencies(typeof(IDataReader))]
    internal partial class InternalClass
    {
        public IDataReader DataReader => _dataReader;
    }
}
