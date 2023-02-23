using I = ExternalProject.Net3_1.Constructors.Sample.InjectableDependencies.IUserReader;

namespace ExternalProject.Net3_1.Constructors.Sample.NamespaceTests
{
    [SlowFox.InjectDependencies(typeof(I))]
    public partial class ReferenceDependencyViaTypeAliasAsI
    {
        public I Dependency => _i;
    }
}
