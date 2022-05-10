namespace ExternalProject.Net6.SampleProject1.BaseClasses
{
    [SlowFox.InjectDependencies(typeof(IDataReader2))]
    public partial class DerivedNestedBaseClassWithDependency : DerivedBaseClassWithMultipleDependencies
    {
        public IDataReader2 DataReader2 => _dataReader2;
    }
}
