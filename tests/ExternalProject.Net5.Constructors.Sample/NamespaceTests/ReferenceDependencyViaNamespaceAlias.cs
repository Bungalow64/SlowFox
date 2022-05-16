using I = ExternalProject.Net5.Constructors.Sample.InjectableDependencies;

namespace ExternalProject.Net5.Constructors.Sample.NamespaceTests
{
    [SlowFox.InjectDependencies(typeof(I.IUserReader))]
    public partial class ReferenceDependencyViaNamespaceAlias
    {
        public I.IUserReader Dependency => _userReader;
    }
}
