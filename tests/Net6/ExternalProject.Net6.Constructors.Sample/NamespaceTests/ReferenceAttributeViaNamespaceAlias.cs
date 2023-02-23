using ExternalProject.Net6.Constructors.Sample.InjectableDependencies;
using I = SlowFox.InjectDependenciesAttribute;

namespace ExternalProject.Net6.Constructors.Sample.NamespaceTests
{
    [I(typeof(IUserReader))]
    public partial class ReferenceAttributeViaNamespaceAlias
    {
        public IUserReader Dependency => _userReader;
    }
}
