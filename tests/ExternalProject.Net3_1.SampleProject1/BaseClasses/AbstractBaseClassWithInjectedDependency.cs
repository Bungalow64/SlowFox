using ExternalProject.Net3_1.SampleProject1.InjectableDependencies;

namespace ExternalProject.Net3_1.SampleProject1.BaseClasses
{
    [SlowFox.InjectDependencies(typeof(IUserReader))]
    public abstract partial class AbstractBaseClassWithInjectedDependency
    {
        public IUserReader UserReader => _userReader;
    }
}
