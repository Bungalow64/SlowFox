using ExternalProject.Net3_1.SampleProject1.InjectableDependencies;
using I = SlowFox;

namespace ExternalProject.Net3_1.SampleProject1.NamespaceTests
{
    [I.InjectDependencies(typeof(IUserReader))]
    public partial class ReferenceAttributeViaTypeAlias
    {
    }
}
