using ExternalProject.Net3_1.Constructors.Sample.InjectableDependencies;
using I = SlowFox.InjectDependenciesAttribute;

namespace ExternalProject.Net3_1.Constructors.Sample.NamespaceTests
{
    [I(typeof(IUserReader))]
    public partial class ReferenceAttributeViaNamespaceAlias
    {
        public IUserReader Dependency => _userReader;
    }
}
