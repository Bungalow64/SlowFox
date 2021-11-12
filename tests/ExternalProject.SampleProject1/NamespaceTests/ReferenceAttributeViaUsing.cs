using ExternalProject.SampleProject1.InjectableDependencies;
using SlowFox;

namespace ExternalProject.SampleProject1.NamespaceTests
{
    [InjectDependencies(typeof(IUserReader))]
    public partial class ReferenceAttributeViaUsing
    {
    }
}
