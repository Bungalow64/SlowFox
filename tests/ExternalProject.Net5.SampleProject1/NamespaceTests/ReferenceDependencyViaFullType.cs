namespace ExternalProject.Net5.SampleProject1.NamespaceTests
{
    [SlowFox.InjectDependencies(typeof(ExternalProject.Net5.SampleProject1.InjectableDependencies.IUserReader))]
    public partial class ReferenceDependencyViaFullType
    {
        public ExternalProject.Net5.SampleProject1.InjectableDependencies.IUserReader Dependency => _userReader;
    }
}
