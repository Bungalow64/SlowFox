using ExternalProject.Net6.Constructors.Sample.InjectableDependencies;

namespace ExternalProject.Net6.Constructors.Sample.BaseClasses
{
    [SlowFox.InjectDependencies(typeof(IUserWriter))]
    public partial class DerivedBaseClassWithInjectedDependency : AbstractBaseClassWithInjectedDependency
    {
        public IUserWriter UserWriter => _userWriter;
    }
}
