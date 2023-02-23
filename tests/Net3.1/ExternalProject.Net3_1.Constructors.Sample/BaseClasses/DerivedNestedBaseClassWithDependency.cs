namespace ExternalProject.Net3_1.Constructors.Sample.BaseClasses
{
    [SlowFox.InjectDependencies(typeof(IDataReader2))]
    public partial class DerivedNestedBaseClassWithDependency : DerivedBaseClassWithMultipleDependencies
    {
        public IDataReader2 DataReader2 => _dataReader2;
    }
}
