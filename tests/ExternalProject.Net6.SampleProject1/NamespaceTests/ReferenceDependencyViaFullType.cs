namespace ExternalProject.Net6.SampleProject1.NamespaceTests
{
    [SlowFox.InjectDependencies(typeof(ExternalProject.Net6.SampleProject1.InjectableDependencies.IUserReader))]
    public partial class ReferenceDependencyViaFullType
    {
    }
}
