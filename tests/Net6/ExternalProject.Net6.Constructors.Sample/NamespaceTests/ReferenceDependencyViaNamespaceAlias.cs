using I = ExternalProject.Net6.Constructors.Sample.InjectableDependencies;

namespace ExternalProject.Net6.Constructors.Sample.NamespaceTests
{
    [SlowFox.InjectDependencies(typeof(I.IUserReader))]
    public partial class ReferenceDependencyViaNamespaceAlias
    {
        public I.IUserReader Dependency => _userReader;
    }
}
