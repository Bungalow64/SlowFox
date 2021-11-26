using ExternalProject.Net5.SampleProject1.InjectableDependencies;
using I = SlowFox.InjectDependenciesAttribute;

namespace ExternalProject.Net5.SampleProject1.NamespaceTests
{
    [I(typeof(IUserReader))]
    public partial class ReferenceAttributeViaNamespaceAlias
    {
    }
}
