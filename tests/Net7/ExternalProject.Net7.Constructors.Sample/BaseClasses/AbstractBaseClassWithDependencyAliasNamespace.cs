using I = ExternalProject.Net7.Constructors.Sample.InjectableDependencies;

namespace ExternalProject.Net7.Constructors.Sample.BaseClasses
{
    public abstract class AbstractBaseClassWithDependencyAliasNamespace
    {
        private readonly I.IUserReader _userReader;

        public AbstractBaseClassWithDependencyAliasNamespace(I.IUserReader userReader)
        {
            _userReader = userReader;
        }

        public I.IUserReader UserReader => _userReader;
    }
}
