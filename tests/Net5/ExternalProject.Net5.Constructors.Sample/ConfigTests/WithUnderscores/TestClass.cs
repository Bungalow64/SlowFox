namespace ExternalProject.Net5.Constructors.Sample.ConfigTests.WithUnderscores
{
    [SlowFox.InjectDependencies(typeof(IDataReader))]
    public partial class TestClass
    {
        public IDataReader DataReader => _dataReader;
    }
}
