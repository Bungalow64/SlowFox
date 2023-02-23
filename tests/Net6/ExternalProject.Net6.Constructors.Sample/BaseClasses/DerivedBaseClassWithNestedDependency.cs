namespace ExternalProject.Net6.Constructors.Sample.BaseClasses
{
    [SlowFox.InjectDependencies(typeof(IDataReader))]
    public partial class DerivedBaseClassWithNestedDependency : AbstractBaseClassWithNestedDependency
    {
        public IDataReader DataReader => _dataReader;
    }
}
