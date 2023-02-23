namespace ExternalProject.Net7.Constructors.Sample.ConfigTests.NoUnderscores
{
    [SlowFox.InjectDependencies(typeof(IDataReader))]
    public partial class TestClass
    {
        public IDataReader DataReader => dataReader;
    }
}
