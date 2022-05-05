using I = ExternalProject.Net3_1.SampleProject1;

namespace ExternalProject.Net3_1.SampleProject1.BaseClasses
{
    public abstract class AbstractBaseClassWithDependencyAliasPartialNamespace
    {
        private readonly I.InjectableDependencies.IUserReader _userReader;

        public AbstractBaseClassWithDependencyAliasPartialNamespace(I.InjectableDependencies.IUserReader userReader)
        {
            _userReader = userReader;
        }

        public I.InjectableDependencies.IUserReader UserReader => _userReader;
    }
}
