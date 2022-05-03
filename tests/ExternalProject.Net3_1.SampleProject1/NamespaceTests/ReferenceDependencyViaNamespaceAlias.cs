using I = ExternalProject.Net3_1.SampleProject1.InjectableDependencies;

namespace ExternalProject.Net3_1.SampleProject1.NamespaceTests
{
    [SlowFox.InjectDependencies(typeof(I.IUserReader))]
    public partial class ReferenceDependencyViaNamespaceAlias
    {
        public I.IUserReader Dependency => _userReader;
    }
}
