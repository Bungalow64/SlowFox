using ExternalProject.Net3_1.SampleProject1.InjectableDependencies;
using SlowFox;

namespace ExternalProject.Net3_1.SampleProject1.NamespaceTests
{
    [InjectDependencies(typeof(IUserReader))]
    public partial class ReferenceAttributeViaUsing
    {
    }
}
