namespace ExternalProject.Net5.SampleProject1.ConfigTests.NoUnderscores
{
    [SlowFox.InjectDependencies(typeof(IDataReader))]
    public partial class TestClass
    {
        public IDataReader DataReader => dataReader;
    }
}
