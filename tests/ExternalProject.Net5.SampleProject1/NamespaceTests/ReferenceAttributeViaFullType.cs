using ExternalProject.Net5.SampleProject1.InjectableDependencies;

namespace ExternalProject.Net5.SampleProject1.NamespaceTests
{
    [SlowFox.InjectDependencies(typeof(IUserReader))]
    public partial class ReferenceAttributeViaFullType
    {
    }
}
