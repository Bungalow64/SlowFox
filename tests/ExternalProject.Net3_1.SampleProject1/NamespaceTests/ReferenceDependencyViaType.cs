using ExternalProject.Net3_1.SampleProject1.InjectableDependencies;

namespace ExternalProject.Net3_1.SampleProject1.NamespaceTests
{
    [SlowFox.InjectDependencies(typeof(IUserReader))]
    public partial class ReferenceDependencyViaType
    {
        public IUserReader Dependency => _userReader;
    }
}
