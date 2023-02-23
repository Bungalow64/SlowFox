using ExternalProject.Net3_1.Constructors.Sample.InjectableDependencies;

namespace ExternalProject.Net3_1.Constructors.Sample.NamespaceTests
{
    [SlowFox.InjectDependencies(typeof(IUserReader))]
    public partial class ReferenceDependencyViaType
    {
        public IUserReader Dependency => _userReader;
    }
}
