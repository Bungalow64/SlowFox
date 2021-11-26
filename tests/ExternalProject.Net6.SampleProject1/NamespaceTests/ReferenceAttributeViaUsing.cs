using ExternalProject.Net6.SampleProject1.InjectableDependencies;
using SlowFox;

namespace ExternalProject.Net6.SampleProject1.NamespaceTests
{
    [InjectDependencies(typeof(IUserReader))]
    public partial class ReferenceAttributeViaUsing
    {
    }
}
