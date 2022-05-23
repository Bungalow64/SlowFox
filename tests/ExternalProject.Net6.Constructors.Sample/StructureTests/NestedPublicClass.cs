namespace ExternalProject.Net6.Constructors.Sample.StructureTests
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
