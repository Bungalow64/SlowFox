using Reader = ExternalProject.Net6.SampleProject1.InjectableDependencies.IUserReader;

namespace ExternalProject.Net6.SampleProject1.NamespaceTests
{
    [SlowFox.InjectDependencies(typeof(Reader))]
    public partial class ReferenceDependencyViaTypeAlias
    {
    }
}
