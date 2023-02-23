using ExternalProject.Net7.Constructors.Sample.InjectableDependencies;

namespace ExternalProject.Net7.Constructors.Sample.BaseClasses
{
    public partial class DerivedBaseClassWithNoneInjected : AbstractBaseClassWithInjectedDependency
    {
        private readonly IDataReader _dataReader;

        public DerivedBaseClassWithNoneInjected(IUserReader userReader, IDataReader dataReader) : base(userReader)
        {
            _dataReader = dataReader;
        }

        public IDataReader DataReader => _dataReader;
    }
}
