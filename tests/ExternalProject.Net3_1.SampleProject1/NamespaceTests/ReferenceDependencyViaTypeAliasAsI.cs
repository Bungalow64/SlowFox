using I = ExternalProject.Net3_1.SampleProject1.InjectableDependencies.IUserReader;

namespace ExternalProject.Net3_1.SampleProject1.NamespaceTests
{
    [SlowFox.InjectDependencies(typeof(I))]
    public partial class ReferenceDependencyViaTypeAliasAsI
    {
        public I Dependency => _i;
    }
}
