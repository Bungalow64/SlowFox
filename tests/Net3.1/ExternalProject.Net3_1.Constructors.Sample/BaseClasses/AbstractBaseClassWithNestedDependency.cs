using ExternalProject.Net3_1.Constructors.Sample.InjectableDependencies;

namespace ExternalProject.Net3_1.Constructors.Sample.BaseClasses
{
    public abstract class AbstractBaseClassWithNestedDependency : AbstractBaseClassWithDependency
    {
        private readonly IUserWriter _userWriter;
        public AbstractBaseClassWithNestedDependency(IUserWriter userWriter, IUserReader userReader) : base(userReader)
        {
            _userWriter = userWriter;
        }

        public IUserWriter UserWriter => _userWriter;
    }
}
