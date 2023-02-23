using I = ExternalProject.Net6.Constructors.Sample.InjectableDependencies.IUserReader;

namespace ExternalProject.Net6.Constructors.Sample.NamespaceTests
{
    [SlowFox.InjectDependencies(typeof(I))]
    public partial class ReferenceDependencyViaTypeAliasAsI
    {
        public I Dependency => _i;
    }
}
