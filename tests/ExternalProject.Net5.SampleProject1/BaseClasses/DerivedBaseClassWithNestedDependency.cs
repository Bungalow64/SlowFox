namespace ExternalProject.Net5.SampleProject1.BaseClasses
{
    [SlowFox.InjectDependencies(typeof(IDataReader))]
    public partial class DerivedBaseClassWithNestedDependency : AbstractBaseClassWithNestedDependency
    {
        public IDataReader DataReader => _dataReader;
    }
}
