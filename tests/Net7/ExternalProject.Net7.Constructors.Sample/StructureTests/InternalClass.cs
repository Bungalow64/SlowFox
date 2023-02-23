namespace ExternalProject.Net7.Constructors.Sample.StructureTests
{
    [SlowFox.InjectDependencies(typeof(IDataReader))]
    internal partial class InternalClass
    {
        public IDataReader DataReader => _dataReader;
    }
}
