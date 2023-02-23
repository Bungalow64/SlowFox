using ExternalProject.Net3_1.Constructors.Sample.InjectableDependencies;

namespace ExternalProject.Net3_1.Constructors.Sample.BaseClasses
{
    [SlowFox.InjectDependencies(typeof(IUserReader))]
    public abstract partial class AbstractBaseClassWithInjectedDependency
    {
        public IUserReader UserReader => _userReader;
    }
}
