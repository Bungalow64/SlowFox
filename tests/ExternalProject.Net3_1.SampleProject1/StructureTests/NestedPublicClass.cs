namespace ExternalProject.Net3_1.SampleProject1.StructureTests
{
    public partial class NestedPublicClass
    {
        [SlowFox.InjectDependencies(typeof(IDataReader))]
        public partial class InnerClass
        {
            public IDataReader DataReader => _dataReader;
        }
    }
}
