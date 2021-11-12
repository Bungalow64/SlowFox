using ExternalProject.SampleProject1.InjectableDependencies;
using I = SlowFox;

namespace ExternalProject.SampleProject1.NamespaceTests
{
    [I.InjectDependencies(typeof(IUserReader))]
    public partial class ReferenceAttributeViaTypeAlias
    {
    }
}
