using ExternalProject.Net6.SampleProject1.InjectableDependencies;
using I = SlowFox;

namespace ExternalProject.Net6.SampleProject1.NamespaceTests
{
    [I.InjectDependencies(typeof(IUserReader))]
    public partial class ReferenceAttributeViaTypeAlias
    {
    }
}
