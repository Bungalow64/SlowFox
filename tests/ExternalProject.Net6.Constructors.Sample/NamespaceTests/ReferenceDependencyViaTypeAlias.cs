using Reader = ExternalProject.Net6.Constructors.Sample.InjectableDependencies.IUserReader;

namespace ExternalProject.Net6.Constructors.Sample.NamespaceTests
{
    [SlowFox.InjectDependencies(typeof(Reader))]
    public partial class ReferenceDependencyViaTypeAlias
    {
        public Reader Dependency => _reader;
    }
}
