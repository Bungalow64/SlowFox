namespace ExternalProject.Net5.Constructors.Sample.ConfigTests.NoUnderscores
{
    [SlowFox.InjectDependencies(typeof(IDataReader))]
    public partial class TestClass
    {
        public IDataReader DataReader => dataReader;
    }
}
