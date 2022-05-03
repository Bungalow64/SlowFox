using Reader = ExternalProject.Net5.SampleProject1.InjectableDependencies.IUserReader;

namespace ExternalProject.Net5.SampleProject1.NamespaceTests
{
    [SlowFox.InjectDependencies(typeof(Reader))]
    public partial class ReferenceDependencyViaTypeAlias
    {
        public Reader Dependency => _reader;
    }
}
