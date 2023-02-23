using I = ExternalProject.Net5.Constructors.Sample.InjectableDependencies.IUserReader;

namespace ExternalProject.Net5.Constructors.Sample.BaseClasses
{
    public abstract class AbstractBaseClassWithDependencyAlias
    {
        private readonly I _userReader;

        public AbstractBaseClassWithDependencyAlias(I userReader)
        {
            _userReader = userReader;
        }

        public I UserReader => _userReader;
    }
}
