namespace ExternalProject.Net6.Constructors.Sample.ConfigTests.NoUnderscores
{
    [SlowFox.InjectDependencies(typeof(IDataReader))]
    public partial class TestClass
    {
        public IDataReader DataReader => dataReader;
    }
}
