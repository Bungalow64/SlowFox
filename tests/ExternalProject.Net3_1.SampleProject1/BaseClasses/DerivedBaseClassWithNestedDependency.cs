namespace ExternalProject.Net3_1.SampleProject1.BaseClasses
{
    [SlowFox.InjectDependencies(typeof(IDataReader))]
    public partial class DerivedBaseClassWithNestedDependency : AbstractBaseClassWithNestedDependency
    {
        public IDataReader DataReader => _dataReader;
    }
}
