namespace ExternalProject.Net7.Constructors.Sample.ConfigTests.WithNoNullCheck
{
    [SlowFox.InjectDependencies(typeof(IDataReader))]
    public partial class TestClass
    {
        public IDataReader DataReader => _dataReader;
    }
}
