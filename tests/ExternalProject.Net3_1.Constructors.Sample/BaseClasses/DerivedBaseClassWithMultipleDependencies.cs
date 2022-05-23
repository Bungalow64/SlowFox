namespace ExternalProject.Net3_1.Constructors.Sample.BaseClasses
{
    [SlowFox.InjectDependencies(typeof(IDataReader))]
    public partial class DerivedBaseClassWithMultipleDependencies : AbstractBaseClassWithMultipleDependencies
    {
        public IDataReader DataReader => _dataReader;
    }
}
