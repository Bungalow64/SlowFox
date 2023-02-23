using ExternalProject.Net6.Constructors.Sample.InjectableDependencies;

namespace ExternalProject.Net6.Constructors.Sample.BaseClasses
{
    [SlowFox.InjectDependencies(typeof(IUserWriter))]
    public partial class DerivedBaseClassWithDependency : AbstractBaseClassWithDependency
    {
        public IUserWriter UserWriter => _userWriter;
    }
}
