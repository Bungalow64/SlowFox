namespace ExternalProject.Net3_1.SampleProject1.ConfigTests.NoUnderscores
{
    [SlowFox.InjectDependencies(typeof(IDataReader))]
    public partial class TestClass
    {
        public IDataReader DataReader => dataReader;
    }
}
