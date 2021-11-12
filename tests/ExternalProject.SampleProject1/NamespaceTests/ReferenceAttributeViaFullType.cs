using ExternalProject.SampleProject1.InjectableDependencies;

namespace ExternalProject.SampleProject1.NamespaceTests
{
    [SlowFox.InjectDependencies(typeof(IUserReader))]
    public partial class ReferenceAttributeViaFullType
    {
    }
}
