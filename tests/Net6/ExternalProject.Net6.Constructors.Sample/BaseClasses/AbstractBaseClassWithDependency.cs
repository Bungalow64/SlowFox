using ExternalProject.Net6.Constructors.Sample.InjectableDependencies;

namespace ExternalProject.Net6.Constructors.Sample.BaseClasses
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
