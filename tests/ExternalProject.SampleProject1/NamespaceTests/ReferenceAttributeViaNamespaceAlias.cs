using ExternalProject.SampleProject1.InjectableDependencies;
using I = SlowFox.InjectDependenciesAttribute;

namespace ExternalProject.SampleProject1.NamespaceTests
{
    [I(typeof(IUserReader))]
    public partial class ReferenceAttributeViaNamespaceAlias
    {
    }
}
