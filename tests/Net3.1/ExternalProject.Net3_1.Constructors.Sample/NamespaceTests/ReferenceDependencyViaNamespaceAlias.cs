using I = ExternalProject.Net3_1.Constructors.Sample.InjectableDependencies;

namespace ExternalProject.Net3_1.Constructors.Sample.NamespaceTests
{
    [SlowFox.InjectDependencies(typeof(I.IUserReader))]
    public partial class ReferenceDependencyViaNamespaceAlias
    {
        public I.IUserReader Dependency => _userReader;
    }
}
