using Reader = ExternalProject.Net7.Constructors.Sample.InjectableDependencies.IUserReader;

namespace ExternalProject.Net7.Constructors.Sample.NamespaceTests
{
    [SlowFox.InjectDependencies(typeof(Reader))]
    public partial class ReferenceDependencyViaTypeAlias
    {
        public Reader Dependency => _reader;
    }
}
