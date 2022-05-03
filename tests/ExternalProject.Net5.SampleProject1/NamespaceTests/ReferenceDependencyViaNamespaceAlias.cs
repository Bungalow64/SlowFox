using I = ExternalProject.Net5.SampleProject1.InjectableDependencies;

namespace ExternalProject.Net5.SampleProject1.NamespaceTests
{
    [SlowFox.InjectDependencies(typeof(I.IUserReader))]
    public partial class ReferenceDependencyViaNamespaceAlias
    {
        public I.IUserReader Dependency => _userReader;
    }
}
