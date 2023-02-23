using I = ExternalProject.Net7.Constructors.Sample;

namespace ExternalProject.Net7.Constructors.Sample.BaseClasses
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
