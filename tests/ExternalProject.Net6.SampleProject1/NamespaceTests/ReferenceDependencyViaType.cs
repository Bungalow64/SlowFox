using ExternalProject.Net6.SampleProject1.InjectableDependencies;

namespace ExternalProject.Net6.SampleProject1.NamespaceTests
{
    [SlowFox.InjectDependencies(typeof(IUserReader))]
    public partial class ReferenceDependencyViaType
    {
        public IUserReader Dependency => _userReader;
    }
}
