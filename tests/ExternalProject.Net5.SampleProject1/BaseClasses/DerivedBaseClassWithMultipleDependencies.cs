namespace ExternalProject.Net5.SampleProject1.BaseClasses
{
    [SlowFox.InjectDependencies(typeof(IDataReader))]
    public partial class DerivedBaseClassWithMultipleDependencies : AbstractBaseClassWithMultipleDependencies
    {
        public IDataReader DataReader => _dataReader;
    }
}
