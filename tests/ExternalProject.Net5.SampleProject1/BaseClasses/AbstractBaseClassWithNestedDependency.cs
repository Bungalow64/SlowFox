using ExternalProject.Net5.SampleProject1.InjectableDependencies;

namespace ExternalProject.Net5.SampleProject1.BaseClasses
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
