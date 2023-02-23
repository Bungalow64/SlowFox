namespace ExternalProject.Net3_1.Constructors.Sample.ConfigTests.WithUnderscores
{
    [SlowFox.InjectDependencies(typeof(IDataReader))]
    public partial class TestClass
    {
        public IDataReader DataReader => _dataReader;
    }
}
