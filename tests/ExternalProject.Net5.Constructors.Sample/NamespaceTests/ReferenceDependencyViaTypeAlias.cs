using Reader = ExternalProject.Net5.Constructors.Sample.InjectableDependencies.IUserReader;

namespace ExternalProject.Net5.Constructors.Sample.NamespaceTests
{
    [SlowFox.InjectDependencies(typeof(Reader))]
    public partial class ReferenceDependencyViaTypeAlias
    {
        public Reader Dependency => _reader;
    }
}
