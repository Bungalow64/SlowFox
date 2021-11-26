namespace ExternalProject.Net3_1.SampleProject1.ConfigTests.UsingDefaults
{
    [SlowFox.InjectDependencies(typeof(IDataReader))]
    public partial class TestClass
    {
        public IDataReader DataReader => _dataReader;
    }
}
