namespace ExternalProject.Net6.SampleProject1.ConfigTests.UsingDefaults
{
    [SlowFox.InjectDependencies(typeof(IDataReader))]
    public partial class TestClass
    {
        public IDataReader DataReader => _dataReader;
    }
}
