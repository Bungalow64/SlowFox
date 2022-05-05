using ExternalProject.Net5.SampleProject1.InjectableDependencies;

namespace ExternalProject.Net5.SampleProject1.BaseClasses
{
    [SlowFox.InjectDependencies(typeof(IUserReader))]
    public abstract partial class AbstractBaseClassWithInjectedDependency
    {
        public IUserReader UserReader => _userReader;
    }
}
