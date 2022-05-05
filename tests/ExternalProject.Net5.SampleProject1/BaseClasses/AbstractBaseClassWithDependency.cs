using ExternalProject.Net5.SampleProject1.InjectableDependencies;

namespace ExternalProject.Net5.SampleProject1.BaseClasses
{
    public abstract class AbstractBaseClassWithDependency
    {
        private readonly IUserReader _userReader;

        public AbstractBaseClassWithDependency(IUserReader userReader)
        {
            _userReader = userReader;
        }

        public IUserReader UserReader => _userReader;
    }
}
