using ExternalProject.Net3_1.Constructors.Sample.InjectableDependencies;

namespace ExternalProject.Net3_1.Constructors.Sample.BaseClasses
{
    [SlowFox.InjectDependencies(typeof(IUserWriter))]
    public partial class DerivedBaseClassWithInjectedDependency : AbstractBaseClassWithInjectedDependency
    {
        public IUserWriter UserWriter => _userWriter;
    }
}
