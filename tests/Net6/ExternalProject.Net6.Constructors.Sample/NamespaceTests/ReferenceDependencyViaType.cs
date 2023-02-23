using ExternalProject.Net6.Constructors.Sample.InjectableDependencies;

namespace ExternalProject.Net6.Constructors.Sample.NamespaceTests
{
    [SlowFox.InjectDependencies(typeof(IUserReader))]
    public partial class ReferenceDependencyViaType
    {
        public IUserReader Dependency => _userReader;
    }
}
