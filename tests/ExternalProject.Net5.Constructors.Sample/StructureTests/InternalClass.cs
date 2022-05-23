namespace ExternalProject.Net5.Constructors.Sample.StructureTests
{
    [SlowFox.InjectDependencies(typeof(IDataReader))]
    internal partial class InternalClass
    {
        public IDataReader DataReader => _dataReader;
    }
}
