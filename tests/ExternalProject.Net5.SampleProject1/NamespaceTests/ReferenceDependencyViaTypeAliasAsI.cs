using I = ExternalProject.Net5.SampleProject1.InjectableDependencies.IUserReader;

namespace ExternalProject.Net5.SampleProject1.NamespaceTests
{
    [SlowFox.InjectDependencies(typeof(I))]
    public partial class ReferenceDependencyViaTypeAliasAsI
    {
        public I Dependency => _i;
    }
}
