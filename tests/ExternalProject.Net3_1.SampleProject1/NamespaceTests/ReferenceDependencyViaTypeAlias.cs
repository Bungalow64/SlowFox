using Reader = ExternalProject.Net3_1.SampleProject1.InjectableDependencies.IUserReader;

namespace ExternalProject.Net3_1.SampleProject1.NamespaceTests
{
    [SlowFox.InjectDependencies(typeof(Reader))]
    public partial class ReferenceDependencyViaTypeAlias
    {
        public Reader Dependency => _reader;
    }
}
