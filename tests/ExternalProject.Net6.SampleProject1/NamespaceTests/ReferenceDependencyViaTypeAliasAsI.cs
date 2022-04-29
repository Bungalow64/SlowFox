using In = ExternalProject.Net6.SampleProject1.InjectableDependencies.IUserReader;

namespace ExternalProject.Net6.SampleProject1.NamespaceTests
{
    [SlowFox.InjectDependencies(typeof(In))]
    public partial class ReferenceDependencyViaTypeAliasAsI
    {
    }
}
