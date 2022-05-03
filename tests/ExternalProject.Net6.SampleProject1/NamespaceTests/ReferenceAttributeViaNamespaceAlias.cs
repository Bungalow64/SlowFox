using ExternalProject.Net6.SampleProject1.InjectableDependencies;
using I = SlowFox.InjectDependenciesAttribute;

namespace ExternalProject.Net6.SampleProject1.NamespaceTests
{
    [I(typeof(IUserReader))]
    public partial class ReferenceAttributeViaNamespaceAlias
    {
        public IUserReader Dependency => _userReader;
    }
}
