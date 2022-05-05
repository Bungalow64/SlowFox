using I = ExternalProject.Net5.SampleProject1.InjectableDependencies.IUserReader;

namespace ExternalProject.Net5.SampleProject1.BaseClasses
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
