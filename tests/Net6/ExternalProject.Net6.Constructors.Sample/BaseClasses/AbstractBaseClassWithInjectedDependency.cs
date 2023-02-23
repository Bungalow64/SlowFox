using ExternalProject.Net6.Constructors.Sample.InjectableDependencies;

namespace ExternalProject.Net6.Constructors.Sample.BaseClasses
{
    [SlowFox.InjectDependencies(typeof(IUserReader))]
    public abstract partial class AbstractBaseClassWithInjectedDependency
    {
        public IUserReader UserReader => _userReader;
    }
}
